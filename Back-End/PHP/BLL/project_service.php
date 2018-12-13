<?php

class project_service extends base_service {

    function get_projects($query) {
        $projects = db_access:: run_reader($query, function ($model) {
                    return $this->init_project($model);
                });
        return $projects;
    }

    function get_projectByIdInit($query) {
        $userProjects = db_access:: run_reader($query, function ($model) {
                    return $this->init_unknown($model);
                });
        return $userProjects;
    }

    function get_all_projects() {
        $query = 'SELECT * FROM task_managment.projects';
        return $this->get_projects($query);
    }

    function get_project_by_id($id) {
        $query = "SELECT up.user_project_id, p.name,allocated_hours, SEC_TO_TIME(SUM(TIME_TO_SEC(end) - TIME_TO_SEC(start))) AS Time "
                . "FROM user_projects UP JOIN projects P ON P.project_id = up.project_id "
                . "LEFT JOIN daily_presence dp ON up.user_project_id = dp.user_project_id "
                . "WHERE up.user_id = {$id} and p.is_complete=0 "
                . "GROUP BY up.user_project_id,p.name,allocated_hours "
                . "ORDER BY dp.user_project_id";

        return $this->get_projectByIdInit($query);
    }

    function add_project($project) {

        $project['startDate'] = date("Y-m-d H:i:s", strtotime($project['startDate']));
        DateTime::createFromFormat("Y-m-d H:i:s", $project['startDate'])->format("d/m/Y");
        $project['endDate'] = date("Y-m-d H:i:s", strtotime($project['endDate']));
        DateTime::createFromFormat("Y-m-d H:i:s", $project['endDate'])->format("d/m/Y");

        $query = " INSERT INTO task_managment.projects"
                . "(name, customer, team_leader, develop_houres, qa_houres, ui_ux_houres, start_date, end_date)"
                . "VALUES ('{$project["name"]}', '{$project["customer"]}',"
                . "'{$project["teamLeaderId"]}', {$project["developHours"]}, {$project["QAHours"]}, {$project["UIUXHours"]},"
                . "'{$project["startDate"]}',"
                . "'{$project["endDate"]}')";

        $result = db_access::run_non_query($query);
        if ($result->affected_rows > 0) {
            return TRUE;
        } else {
            return FALSE;
        }
    }

    //is complete not work!!!
    function update_project_details($project) {
        $isComplete = $project["isComplete"] == true ? 1 : 0;
        $endDate = date("Y-m-d H:i:s", strtotime($project['endDate']));
        DateTime::createFromFormat("Y-m-d H:i:s", $endDate)->format("d/m/Y");
        $query = "UPDATE task_managment.projects "
                . "SET develop_houres={$project["developHours"]},qa_houres={$project["QAHours"]},ui_ux_houres={$project["UIUXHours"]}"
                . ",end_date='{$project["endDate"]}',is_complete=$isComplete "
                . "WHERE project_id={$project["projectId"]}";
        $result = db_access::run_non_query($query)->affected_rows;
        if ($result == -1) {
            return TRUE;
        } else {
            return FALSE;
        }
    }

    function add_workers_to_project($ids, $projectName) {
        $query = "SELECT project_id FROM projects WHERE name = '{$projectName}'";
        $projectid = db_access::run_scalar($query);
        for ($i = 0; $i < count($ids); $i++) {
            $query = "INSERT INTO task_managment.user_projects(user_id,project_id) VALUES({$ids[$i]},{$projectid})";
            db_access::run_non_query($query);
        }
        return true;
    }

}
