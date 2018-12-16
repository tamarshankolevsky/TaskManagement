<?php

class User implements JsonSerializable {
    
#-----------PROPERTIES----------
    
    public $Id;
    public $Name;
    public $UserName;
    public $Password;
    public $StatusId;
    public $EMail;
    public $ManagerId;
    
#-------------METHODS------------

    public function __construct($sqlRaw_) {
        
         $this->Id = $sqlRaw_['user_id'];
         $this->Name = $sqlRaw_['name']; 
         $this->UserName = $sqlRaw_['user_name']; 
         $this->Password = $sqlRaw_['password']; 
         $this->EMail = $sqlRaw_['email'];
         $this->$EMail = $sqlRaw_['$EMail']; 
         $this->ManagerId = $sqlRaw_['manager']; 

    }
    
    public function jsonSerialize() {
        return get_object_vars($this);
    }
}

function test_variables() {
    
}