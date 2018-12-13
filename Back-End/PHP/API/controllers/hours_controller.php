<?php

class hours_controller {

    var $hours_service;

    function __construct() {
        $this->hours_service = new hours_service();
    }

    function get_workers_hours($params) {
        return $this->hours_service->get_workers_hours($params);
    }

    function get_worker_hours($teamLeadetId,$workerId) {
        return $this->hours_service->get_worker_hours($teamLeadetId,$workerId);
    }

     function update_start_hour($idProjectWorker, $hour,$isFirst) {
         if($isFirst==true)
        return $this->hours_service->update_start_hour($idProjectWorker, $hour);
    else 
        return $this->hours_service->update_end_hour($idProjectWorker, $hour);
    }
    
    function send_email($sub,$body,$id) {
        return $this->hours_service->send_email($sub,$body,$id);
    }
}
