<?php

class base_service {

    function init_user($user) {
        // print_r($user);
        $new_user = array();
        $new_user['Id'] = $user['user_id'];
        $new_user['Name'] = $user['name'];
        $new_user['UserName'] = $user['user_name'];
        $new_user['Password'] = $user['password'];
        $new_user['StatusId'] = $user['status'];
        $new_user['EMail'] = $user['email'];
        $new_user['ManagerId'] = $user['manager'];
        $new_user['IsActive'] = $user['is_active'];

//        if (array_key_exists('statuses', $user)) {
//            $new_user['status'] = array();
//            $new_user['status']['statusId'] = $user['status_id'];
//            $new_user['status']['name'] = $user['name'];
//        }
        return $new_user;
    }

    function init_project($project) {
        $new_project = array();
        $new_project['Id'] = $project['project_id'];
        $new_project['Name'] = $project['name'];
        $new_project['Customer'] = $project['customer'];
        $new_project['TeamLeaderId'] = $project['team_leader'];
        $new_project['DevelopHours'] = $project['develop_houres'];
        $new_project['QAHours'] = $project['qa_houres'];
        $new_project['UiUxHours'] = $project['ui_ux_houres'];
        $new_project['StartDate'] = $project['start_date'];
        $new_project['EndDate'] = $project['end_date'];
        $new_project['IsComplete'] = $project['is_complete'];
        return $new_project;
    }

    function init_status($status) {
        $new_status = array();
        $new_status['Id'] = $status['status_id'];
        $new_status['Name'] = $status['name'];
        return $new_status;
    }

    function init_department($department) {
        $new_department = array();
        $new_department['id'] = $department['id'];
        $new_department['department'] = $department['department'];
        return $new_department;
    }

    function init_worker_hours($worker_hours) {
        //hours!!!! convert
        $new_worker_hours = array();
        $new_worker_hours['Name'] = $worker_hours['name'];
        //$new_worker_hours['Date'] = $worker_hours['date'];
        $new_worker_hours['Hours'] = $worker_hours['Time'];
        $new_worker_hours['AllocatedHours'] = $worker_hours['allocated_hours'];
        return $new_worker_hours;
    }

    function init_unknown($unknown) {
        //hours!!!! convert
        $new_worker_hours = array();
        $new_worker_hours['Id'] = $unknown['user_project_id'];
        $new_worker_hours['Name'] = $unknown['name'];
        $new_worker_hours['allocatedHours'] = $unknown['allocated_hours'];
        $new_worker_hours['Hours'] = $unknown['Time'];
        return $new_worker_hours;
    }

    function init_department_hours($department_hours) {
        // $new_department_hours = array();
        // $new_department_hours['departmentHoursId'] = $department_hours['department_hours_id'];
        // $new_department_hours['projectId'] = $department_hours['project_id'];
        // $new_department_hours['departmentId'] = $department_hours['department_id'];
        // $new_department_hours['numHours'] = $department_hours['num_hours'];
        //$new_department_hours['department'] = array();
        // $new_department_hours['department']['departmentName'] = $department_hours['department_name'];
        // return $new_department_hours;
    }

    function init_actual_hours($actual_hours) {
        //return $actual_hours;
        $new_actual_hours = array();
        $new_actual_hours['Id'] = $actual_hours['user_project_id'];
        $new_actual_hours['Name'] = $actual_hours['allocated_hours'];
        $new_actual_hours['Hours'] = $actual_hours['num_hours']; //--change name column in sql query
        return $new_actual_hours;
    }

    function init_details_worker_in_projects($details_worker_in_projects) {
        $new_details_worker_in_projects = array();
        $new_details_worker_in_projects['Hours'] = $details_worker_in_projects['allocated_hours'];
        $new_details_worker_in_projects['Status'] = $details_worker_in_projects['name'];
        $new_details_worker_in_projects['Name'] = $details_worker_in_projects['user_name'];
        $new_details_worker_in_projects['UserId'] = $details_worker_in_projects['user_id'];
        $new_details_worker_in_projects['TeamLeaderName'] = $details_worker_in_projects['teamLeaderName'];
        $query = "SELECT up.user_project_id, p.name , allocated_hours , SEC_TO_TIME(SUM(TIME_TO_SEC(end) - TIME_TO_SEC(start)))"
                . " FROM user_projects UP  join projects p on p.project_id=up.project_id"
                . " LEFT join daily_presence DP on up.user_project_id= dp.user_project_id"
                . " where  p.project_id={$new_tree_table['Id']} and up.user_id= {$new_tree_table['Id']}"
                . " group by up.user_project_id, p.name,allocated_hours;";
        $new_details_worker_in_projects['ActualHours'] = db_access:: run_reader($query, function ($model) {
                    return $this->init_actual_hours($model);
                });
        return $new_details_worker_in_projects;
    }

    function init_tree_table($tree_table) {
        $new_tree_table = array();
        $new_tree_table['Name'] = $tree_table['name'];
        $new_tree_table['Id'] = $tree_table['project_id'];
        $new_tree_table['Name'] = $tree_table['name'];
        $new_tree_table['Customer'] = $tree_table['customer'];
        $new_tree_table['TeamLeaderId'] = $tree_table['team_leader'];
        $new_tree_table['DevelopHours'] = $tree_table['develop_houres'];
        $new_tree_table['QAHours'] = $tree_table['qa_houres'];
        $new_tree_table['UiUxHours'] = $tree_table['ui_ux_houres'];
        $new_tree_table['StartDate'] = $tree_table['start_date'];
        $new_tree_table['EndDate'] = $tree_table['end_date'];
        $new_tree_table['Id'] = $tree_table['user_id']; ////////!!!!!!!!!!!!!???
        $new_tree_table['userName'] = $tree_table['user_name']; ////////////!!!!!!!!!!!!!!!!!!
        $qu = "select allocated_hours,s.name,u.user_name,u.user_id,us.user_name as teamLeaderName"
                . " from users u join users us on u.manager=us.user_id "
                . "join user_projects up on up.user_id = u.user_id  join statuses s "
                . "on s.status_id = u.status where up.project_id ={$new_tree_table['Id']}; ";
        // $new_tree_table['detailsWorkerinProjects'] = $this->get_detailsWorkerinProjects($qu);
        return $new_tree_table;
    }

    function send_email($to, $from, $subject, $body) {
        ini_set('smtp_server', 'smtp.gmail.com');
        ini_set('smtp_port', '587');
        ini_set('auth_username', 'rachel.novak@seldatinc.com');
        ini_set('auth_password', '0556774766');
        ini_set('force_sender', 'rachel.novak@seldatinc.com');
        ini_set('error_logfile', 'error.log');
        ini_set('debug_logfile', 'debug.log');

        $headers = "From: " . $from . "\r\n";
        $headers .= "Reply-To: " . $from . "\r\n";
        $headers .= "CC: susan@example.com\r\n";
        $headers .= "MIME-Version: 1.0\r\n";
        $headers .= "Content-Type: text/html; charset=UTF-8\r\n";
        return mail($to, $subject, $body, $headers);
    }

    function get_detailsWorkerinProjects($qu) {
        return $qu;
        $details = db_access:: run_reader($qu, function ($model) {

                    return $this->init_details_worker_in_projects($model);
                });
        return $details;
    }

}
