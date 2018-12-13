<?php

class report_service extends base_service{

     function get_tree_table_model($query) {
        $treeTable = db_access:: run_reader($query, function ($model) {
                    return $this->init_tree_table($model);
                });
        return $treeTable;
    }
    
    function getTreeTable() {
         $query = "SELECT p.*,user_id,user_name FROM task_managment.projects P  JOIN task_managment.users u ON u.user_id=p.team_leader";
       return $this->get_tree_table_model($query);
    }
}
