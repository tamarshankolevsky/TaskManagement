<?php

class user_controller {

    var $user_service;

    function __construct() {
        $this->user_service = new user_service();
    }

    function login_by_password($password,$user_name){
	return $this->user_service->login_by_password($user_name,$password);
    }

    function get_all_users() {
        return $this->user_service->get_all_users();
    }

    function get_all_team_users($team_leader_id) {
       // return $this->user_service->get_all_team_users($team_leader_id);
    }

    function get_all_teamLeaders() {
        return $this->user_service->get_all_teamLeaders();
    }

    function get_user_by_id($user_id) {
       // return $this->user_service->get_user_by_id($user_id);
    }
    
    function login_by_ip($ip) {
        return $this->user_service->login_by_ip($ip);
    }
    
    function  forgot_password($user_name)
    {
        echo 'send controller';
        return $this->user_service->forgot_password($user_name);
    }
      function add_user($user) {
        return $this->user_service->add_user($user);
    }
    
    function update_user($user) {
        return $this->user_service->update_user($user);
    }
    
    function delete_user($userId) {
        return $this->user_service->delete_user($userId);
    }
    
    function get_user($userId) {
        return $this->user_service->get_user($userId);
    }
    
    function get_workers_datails($teamLeaderId) {
        return $this->user_service->get_workers_datails($teamLeaderId);
    }
    function login($userName,$password) {
        return $this->user_service->login($userName,$password);
    }
}