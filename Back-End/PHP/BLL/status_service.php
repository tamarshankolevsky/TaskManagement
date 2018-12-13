<?php

class status_service extends base_service {

    function get_statuses($query) {
        $statuses = db_access:: run_reader($query, function ($model) {
                    return $this->init_status($model);
                });
        return $statuses;
    }

    function get_all_statuses() {
        $query = 'SELECT * FROM task_managment.statuses';
        return $this->get_statuses($query);
    }
    
    
}
