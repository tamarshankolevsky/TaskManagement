<?php

class hours_service extends base_service {

    function get_hours($query) {
        $worker_hours = db_access:: run_reader($query, function ($model) {
                    return $this->init_worker_hours($model);
                });
        return $worker_hours;
    }

    function get_worker_hours_init($query) {
        $userHours = db_access:: run_reader($query, function ($model) {
                    return $this->init_unknown($model);
                });
        return $userHours;
    }
    
    function get_workers_hours($projectId) {
        $query = "SELECT name,SEC_TO_TIME(SUM(TIME_TO_SEC(end) - TIME_TO_SEC(start))) AS Time,allocated_hours "
                . "FROM users u JOIN user_projects up ON u.user_id=up.user_id "
                . "LEFT JOIN daily_presence dp "
                . "ON up.user_project_id= dp.user_project_id "
                . "WHERE up.project_id= {$projectId} "
                . "GROUP BY name,allocated_hours "
                . "ORDER BY name";
        return $this->get_hours($query);
    }

    function get_worker_hours($teamLeaderId, $workerId) {
        $query = "SELECT up.user_project_id, p.name , allocated_hours , SEC_TO_TIME(SUM(TIME_TO_SEC(end) - TIME_TO_SEC(start))) AS Time "
                . "FROM user_projects up JOIN projects p "
                . "ON p.project_id=up.project_id  "
                . "LEFT JOIN daily_presence dp "
                . "ON up.user_project_id= dp.user_project_id "
                . "WHERE p.team_leader={$teamLeaderId} AND up.user_id= {$workerId} "
                . "GROUP BY up.user_project_id, p.name,allocated_hours";
        return $this->get_worker_hours_init($query);
    }

    function update_start_hour($idProjectWorker, $hour) {
        $datetime = new DateTime();
        $year = $datetime->format('y');
        $month = $datetime->format('m');
        $day = $datetime->format('d');
        $hour = new DateTime($hour);
        $hour = $hour->format('H:i:s');
        $query = "INSERT INTO task_managment.daily_presence (user_project_id,date,start) "
                . " VALUES({$idProjectWorker}, '{$year}-{$month}-{$day}'"
                . ", '{$hour}');";

        $result = db_access::run_non_query($query);
        if ($result->affected_rows > 0) {
            return TRUE;
        } else {
            return FALSE;
        }
    }

    function update_end_hour($idProjectWorker, $hour) {
        $hour = new DateTime($hour);
        $hour = $hour->format('H:i:s');
        $query = "UPDATE task_managment.daily_presence  SET end='{$hour}'  WHERE user_project_id={$idProjectWorker}"
                . " AND end IS NULL";
        $result = db_access::run_non_query($query);
        if ($result->affected_rows > 0) {
            return TRUE;
        } else {
            return FALSE;
        }
    }
//
//    function send_email($sub, $body, $id) {
//        $query = "SELECT manager FROM task_managment.users where user_id = {$id}";
//        $managerId = run_scalar($query);
//        $query = "SELECT email FROM task_managment.users where user_id = {$managerId}";
//        $email = run_scalar($query);
//        return base_service::send_email(sub, body, email);
//    }

}
