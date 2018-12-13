<?php

class project_controller {
    
    var $project_service;

    function __construct() {
        $this->project_service = new project_service();
    }

    function get_all_projects(){
	return $this->project_service->get_all_projects();
    }
    
    function add_project($project)
    {
      return  $this->project_service->add_project($project);
    }
    
    function update_project_details($params)
    {
          return  $this->project_service->update_project_details($params);
    }
    
    function get_project_by_id($projectId)
    {
          return  $this->project_service->get_project_by_id($projectId);
    }
    
    function add_workers_to_project($ids,$projectName){
        return  $this->project_service->add_workers_to_project($ids,$projectName);
    }
}
