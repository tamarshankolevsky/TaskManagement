<?php

class db_access {

    static function run_reader($query, $init_model) {

        global $connection;

        $resultObj = $connection->query($query);


        /* create one master array of the records */
        $list = array();

        while ($singleRowFromQuery = $resultObj->fetch_array(MYSQLI_ASSOC)) {
            $list[] = $init_model($singleRowFromQuery);
        }
        return $list;
    }

    static function run_scalar($query) {
        global $connection;
        $resultObj = $connection->query($query);
        return array_values(get_object_vars($resultObj->fetch_object()))[0];
        ;
    }
    
    
   static function run_non_query($query) {
        global $connection;
        $resultObj = $connection->query($query);
        return $connection;
    } 

    static function commit() {
        mysql_query("COMMIT");
    }

    static function rollback() {
        mysql_query("ROLLBACK");
    }

}
