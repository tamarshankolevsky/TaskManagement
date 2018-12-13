<?php

class user_service extends base_service {

    // function get_users_query() {
    //  $query = "SELECT u.*,department_name,tl.user_name as team_leader_name FROM task_management.user u " .
    //      "LEFT JOIN task_management.department d on u.department_id=d.department_id " .
    //     "LEFT JOIN task_management.user tl ON u.team_leader_id=tl.user_id " .
    //     "WHERE u.is_active=1 ";
    // return $query;
    // }

    function get_users($query) {
        $users = db_access:: run_reader($query, function ($model) {
                    return $this->init_user($model);
                });
        return $users;
    }

    function login($userName, $password) {
        $query = "SELECT * FROM task_managment.users WHERE user_name='{$userName}' and password='{$password}' and is_active=1";
        return $this->get_users($query)[0];
    }

    function get_all_users() {
        $query = 'SELECT * FROM task_managment.users where is_active=1';
        return $this->get_users($query);
    }
    
    function get_all_teamLeaders() {
        $query = 'SELECT * FROM task_managment.users where status=2 and is_active=1';
        return $this->get_users($query);
    }
    

    function project_manager($team_leader_id) {
        $query = "SELECT * FROM managertasks.project WHERE managerId =$team_leader_id";
        return $this->get_users($query)[0];
    }

    function projects_user($user_id) {
        $query = "SELECT *,(select sum(sumHours) from presentday pd where pd.id=pw.id and pd.projectId=p.projectId group by id) as sumHoursDone FROM managertasks.projectworker pw join project p on  pw.projectId = p.projectId where pw.id = $user_id and p.isFinish=false ";
        return $this->get_users($query)[0];
    }

    function user_details() {
        $query = "SELECT * FROM managertasks.user JOIN managertasks.department ON user.departmentUserId=department.id WHERE user.id={id}";
        return $this->get_users($query)[0];
    }

    function add_user($user) {

        if ($user['managerId'] != null)
            $query = "INSERT INTO task_managment.users "
                    . "(name,user_name,password,email,status,manager)"
                    . " VALUES ('{$user['name']}','{$user['userName']}',"
                    . "'{$user['Password']}','{$user['email']}',{$user['statusId']},{$user['managerId']})";
        else {
            $query = "INSERT INTO task_managment.users "
                    . "(name,user_name,password,email,status,manager)"
                    . "VALUES ('{$user['Name']}','{$user['UserName']}',"
                    . "'{$user['Password']}','{$user['email']}',{$user['StatusId']},null)";
        }
        $result = db_access::run_non_query($query)->affected_rows;
        if ($result > 0) {
            return TRUE;
        } else {
            return FALSE;
        }
    }

    function update_user($params) {
        if ($params['managerId'] != null)
            $query = "UPDATE task_managment.users SET name='{$params['name']}', user_name='{$params['userName']}'"
                    . " ,password='{$params['Password']}' , email='{$params['email']}', status={$params['statusId']}, manager={$params['managerId']} WHERE user_id={$params['Id']}";
        else
            $query = "UPDATE task_managment.users SET name='{$params['name']}', user_name='{$params['userName']}', email='{$params['email']}', "
                    . ",password='{$params['Password']}' ,status={$params['statusId']},manager=null WHERE user_id={$params['Id']}";
        $result = db_access::run_non_query($query);

        if ($result != null) {
            return TRUE;
        } else {
            echo $result;
            return FALSE;
        }
    }

    function delete_user($userId) {
        $query = "UPDATE task_managment.users SET is_active=0 where user_id={$userId["userId"]} AND is_active=1;";
        $result = db_access::run_non_query($query);
        if ($result != null) {
            return TRUE;
        } else {
            return FALSE;
        }
    }

    function get_user($userId) {
        $query = "SELECT * FROM task_managment.users WHERE user_id={$userId["userId"]};";
        return $this->get_users($query)[0];
    }

    public function init_department($department) {
        //////////////////////////////
    }

    function get_workers_datails($params) {
        $query = "SELECT * FROM task_managment.users "
                . "WHERE manager={$params["params"]} and is_active=1;";
        return $this->get_users($query);
    }

    
}
