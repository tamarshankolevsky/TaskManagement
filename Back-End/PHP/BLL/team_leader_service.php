<?php

class team_leader_service extends base_service {

    function get_workers($query) {
        $workers = db_access:: run_reader($query, function ($model) {
                    return $this->init_user($model);
                });
        return $workers;
    }

    function get_workers_for_project($teamLeaderId) {
        $query = "SELECT u.* "
                . "FROM users u "
                . "JOIN user_projects up ON u.user_id= up.user_id "
                . "WHERE project_id IN ("
                . "SELECT project_id FROM projects "
                . "WHERE team_leader= {$teamLeaderId}"
                . ") "
                . "GROUP BY u.user_name;";
        return $this->get_workers($query);
    }

    function update_worker_hours($projectWorkerId, $numHours) {
        $query = "UPDATE task_managment.user_projects "
                . "SET allocated_hours={$numHours} "
                . "WHERE user_project_id={$projectWorkerId}";
        return db_access:: run_non_query($query);
    }

    function get_statuses($query) {

        $statuses = db_access:: run_reader($query, function ($model) {
                    return $model;
                    //return $this->init_status($model);
                });
        return $statuses;
    }

    function get_all_statuses() {
        $query = 'SELECT * FROM task_managment.statuses';
        return $this->get_statuses($query);
    }

    function get_remaining_hours($projectId, $statusId) {

        $statusName = $this->get_all_statuses();
        foreach ($statusName as $rkey => $resource) {

            if ($resource['status_id'] == $statusId) {

                $statusName = $resource['name'];
            }
        }

        switch ($statusName) {
            case "developer": {
                    $statusName = "develop_houres";
                    break;
                }
            case "QA": {
                    $statusName = "qa_houres";
                    break;
                }
            case "UxUi": {
                    $statusName = "ui_ux_houres";
                    break;
                }
        }

        $query = "SELECT {$statusName} - SUM(allocated_hours)"
                . " FROM projects P JOIN  user_projects PW ON P.project_id = PW.project_id JOIN users W ON W.user_id = PW.user_id"
                . " WHERE PW.user_project_id = {$projectId} AND W.status = {$statusId} ";
        return db_access::run_scalar($query);
    }

    function send_email_manager($subject, $body, $user_id) {
        $query = "SELECT manager FROM task_managment.users where user_id = {$user_id}";
        $managerId = db_access::run_scalar($query);
        $query = "SELECT email FROM task_managment.users where user_id = {$managerId}";
        $teamLeaderEmail = db_access::run_scalar($query);
        $query = "SELECT email FROM task_managment.users where user_id = {$user_id}";
        $userEemail = db_access::run_scalar($query);

        return $this->send_email($userEemail, $teamLeaderEmail, $subject, $body);
    }


}
