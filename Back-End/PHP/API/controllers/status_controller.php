<?php

class status_controller {
    
    var $status_service;

    function __construct() {
        $this->status_service = new status_service();
    }

    function get_all_statuses(){
	return $this->status_service->get_all_statuses();
    }
    
}
