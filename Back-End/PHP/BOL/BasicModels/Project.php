<?php

class User implements JsonSerializable {
    
#-----------PROPERTIES----------
    
    public $Id;
    public $Name;
    public $TeamLeaderId;
    public $Customer;
    public $DevelopHours;
    public $QAHours;
    public $UiUxHours;
    public $StartDate;
    public $EndDate;
    public $IsComplete;
    
#------------METHODS-----------

    public function __construct($sqlRaw_) {
        
         $this->Id = $sqlRaw_['project_id'];
         $this->Name = $sqlRaw_['name']; 
         $this->TeamLeaderId = $sqlRaw_['customer']; 
         $this->Customer = $sqlRaw_['team_leader']; 
         $this->DevelopHours = $sqlRaw_['develop_hours'];
         $this->QAHours = $sqlRaw_['qa_hours']; 
         $this->UiUxHours = $sqlRaw_['ui_ux_hours']; 
         $this->StartDate = $sqlRaw_['start_date'];
         $this->EndDate = $sqlRaw_['end_date']; 
         $this->IsComplete = $sqlRaw_['is_complete'];
         
    }
    
    public function jsonSerialize() {
        return get_object_vars($this);
    }
}

function test_variables() {
    
}