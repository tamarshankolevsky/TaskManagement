<?php

class routes_loader {

    var $methods;
    var $user_controller;
    var $project_controller;
    var $status_controller;
    var $hours_controller;
    var $team_leader_controller;
    var $report_controller;
    var $customer_controller;
    var $department_controller;
    var $project_worker_controller;
    var $presence_hours_controller;

    public function __construct() {
        $this->user_controller = new user_controller();
        $this->project_controller = new project_controller();
        $this->status_controller = new status_controller();
        $this->hours_controller = new hours_controller();
        $this->team_leader_controller = new team_leader_controller();
        $this->report_controller = new report_controller();

        $this->methods = array(
            'user' => $this->get_users_methods(),
            'project' => $this->get_projects_methods(),
            'status' => $this->get_statuses_methods(),
            'hours' => $this->get_hours_methods(),
            'teamLeader' => $this->get_team_leader_methods(),
            'report' => $this->get_report_methods(),
            'department' => $this->get_departments_methods(),
        );
    }

    function invoke($controller_name, $method_name, $params) {
        $data = $this->methods[$controller_name][$method_name]($params);
        echo json_encode($data, JSON_NUMERIC_CHECK);
    }

    function get_users_methods() {
        return array(
            'getAllUsers' => function ($params) {
                return $this->user_controller->get_all_users();
            },
            ///////////////////// 
            'forgetPassword' => function ($params) {
                return $this->user_controller->forgot_password($params['userName']);
            },
            'getAllTeamLeaders' => function ($params) {
                return $this->user_controller->get_all_teamLeaders();
            },
            'getUserById' => function ($params) {
                // return $this->user_controller->get_user_by_id($params['userId']);
            },
            'loginByPassword' => function ($params) {
                return $this->user_controller->login_by_password($params['password'], $params['userName']);
            },
            'loginByIp' => function ($params) {
                return $this->user_controller->login_by_ip($params["ip"]);
            },
            'addUser' => function ($params) {
                return $this->user_controller->add_user($params);
            },
            'updateUser' => function ($params) {
                return $this->user_controller->update_user($params);
            },
            'deleteUser' => function ($userId) {
                return $this->user_controller->delete_user($userId);
            },
            'getUser' => function ($params) {
                return $this->user_controller->get_user($params);
            },
            'getWorkersDeatails' => function ($params) {
                return $this->user_controller->get_workers_datails($params);
            },
            'login' => function ($params) {
                return $this->user_controller->login($params['userName'], $params['password']);
            }
        );
    }

    function get_projects_methods() {
        return array(
            'getAllProjects' => function ($params) {
                return $this->project_controller->get_all_projects();
            },
            'addProject' => function ($params) {

                return $this->project_controller->add_project($params);
            },
            'updateProjectDetails' => function ($params) {

                return $this->project_controller->update_project_details($params);
            },
            ///////////////////////     
            'getProjectById' => function ($params) {
                // return $this->project_controller->get_project_by_id($params['projectId']);
            },
            'getProjectsByTeamLeaderId' => function ($params) {
                // return $this->project_controller->get_project_by_team_leader_id($params['teamLeaderId']);
            },
            'getProjectsReports' => function () {
                // return $this->project_controller->get_projects_reports();
            },
            'getAllStatuses' => function ($params) {
                return $this->project_controller->get_all_projects(); ///////////////the problem in the func get all... init status???!
            },
            'getProject' => function ($params) {
                return $this->project_controller->get_project_by_id($params["params"]); ///////////////the problem in the func get all... init status???!
            }
            ,
            'addWorkersToProject' => function ($params) {

                return $this->project_controller->add_workers_to_project($params["ids"], $params["projectName"]); ///////////////the problem in the func get all... init status???!
            }
        );
    }

    function get_statuses_methods() {
        return array(
            'getAllStatuses' => function ($params) {
                return $this->status_controller->get_all_statuses();
            }
        );
    }

    function get_hours_methods() {
        return array(
            'getWorkersHours' => function ($params) {
                return $this->hours_controller->get_workers_hours($params["params"]);
            },
            'getWorkerHours' => function ($params) {
                return $this->hours_controller->get_worker_hours($params["teamLeaderId"], $params["workerId"]);
            },
            'updateStartHour' => function($params) {
                return $this->hours_controller->update_start_hour($params["idProjectWorker"], $params["hour"], $params["isFirst"]);
            }
        );
    }

    function get_team_leader_methods() {
        return array(
            'getWorkersForProject' => function ($id) {
                return $this->team_leader_controller->get_workers_for_project($id["id"]);
            },
            'updateWorkerHours' => function ($params) {
                return $this->team_leader_controller->update_worker_hours($params["projectWorkerId"], $params["numHours"]);
            },
            'getRemainingHours' => function ($params) {
                return $this->team_leader_controller->get_remaining_hours($params["userProjectid"], $params["statusId"]);
            },
            'sendMessageToManagers' => function ($params) {
                return $this->team_leader_controller->send_email_manager($params["subject"], $params["body"], $params["userId"]);
            }
        );
    }

    function get_report_methods() {
        return array(
            'getTreeTable' => function ($params) {
                return $this->report_controller->getTreeTable();
            }
        );
    }

    function get_departments_methods() {
        return array(
            'getAllDepartments' => function ($params) {
                return $this->department_controller->get_all_departments();
            }
        );
    }

    function get_project_worker_methods() {
        return array(
            'getAllWorkerHours' => function ($params) {
                //  return $this->worker_hours_controller->getAllWorkerHours($params['workerId']);
            }
        );
    }

    function get_presence_day_methods() {
        return array(
            'GetPresenceStatusPerWorkers' => function ($params) {

                // return $this->$presence_hours_controller->get_presence_status_per_workers($params['teamLeaderId']);
            }
        );
    }

}
