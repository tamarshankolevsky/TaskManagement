<?php

class team_leader_controller {

    var $team_leader_service;

    function __construct() {
        $this->team_leader_service = new team_leader_service();
    }

    function get_workers_for_project($teamLeaderId) {
        return $this->team_leader_service->get_workers_for_project($teamLeaderId);
    }

    function update_worker_hours($projectWorkerId, $numHours) {
        return $this->team_leader_service->update_worker_hours($projectWorkerId, $numHours);
    }

    function get_remaining_hours($projectId, $statusId) {
        return $this->team_leader_service->get_remaining_hours($projectId, $statusId);
    }

    function send_email_manager($subject, $body, $user_id) {
        return $this->team_leader_service->send_email_manager($subject, $body, $user_id);
    }

}
