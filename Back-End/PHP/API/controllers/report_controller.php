<?php

class report_controller {
    
    var $report_service;

    function __construct() {
        $this->report_service = new report_service();
    }
    
    function getTreeTable(){
	return $this->report_service->getTreeTable();
    }
}

