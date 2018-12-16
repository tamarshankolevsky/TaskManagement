<?php

require_once '../BOL/BasicModels/User.php';

header("Access-Control-Allow-Origin: *");
header("Access-Control-Request-Headers: *");

$connection;
$funcation = $_GET['funcation'];

switch ($funcation) {
    //manager
    case "getAllWorkers":
        getAllWorkers();
        break;
    case "getAllManagers":
        getAllManagers();
        break;
   
    case "updateWorker":
        getAllManagers();
        break;   
    ///
    case "getAllTeamLeaders":
        break;
    case "editWorker":
        editWorker();
        break;
    default:
        var_dump(http_response_code(404));
        die("no such function");
}
#############################
function getAllWorkers() {
    global $connection;
    connection();
    $query = "SELECT * FROM task_managment.workers";
    $resultObj = $connection->query($query);
    $users = array();
    $CleintUser = array();
    $i = 0;
    while ($singleRowFromQuery = $resultObj->fetch_array(MYSQLI_ASSOC)) {
        $users[] = array('users' => $singleRowFromQuery);
        $CleintUser[$i] = new User($singleRowFromQuery);
        $i++;
    }
    header('Content-type: application/json');
    die(json_encode($CleintUser));
}
###############################

function getAllManagers() {
    global $connection;
    connection();
    $query = "SELECT * FROM task_managment.workers WHERE status<=2";
    $resultObj = $connection->query($query);
    $users = array();
    $CleintUser = array();
    $i = 0;
    while ($singleRowFromQuery = $resultObj->fetch_array(MYSQLI_ASSOC)) {
        $users[] = array('users' => $singleRowFromQuery);
        $CleintUser[$i] = new User($singleRowFromQuery);
        $i++;
    }
    header('Content-type: application/json');
    die(json_encode($CleintUser));
}

function updateWorker() {
    global $connection;
    connection();
    header('Content-type: application/json');
    header('Access-Control-Allow-Origin: *');
    header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
     // Allow from any origin
    if (isset($_SERVER['HTTP_ORIGIN'])) {
        header("Access-Control-Allow-Origin: {$_SERVER['HTTP_ORIGIN']}");
        header('Access-Control-Allow-Credentials: true');
        header('Access-Control-Max-Age: 86400');    // cache for 1 day
    }
    // Access-Control headers are received during OPTIONS requests
    if ($_SERVER['REQUEST_METHOD'] == 'OPTIONS') {
        if (isset($_SERVER['HTTP_ACCESS_CONTROL_REQUEST_METHOD']))
            header("Access-Control-Allow-Methods: GET, POST, OPTIONS");         
        if (isset($_SERVER['HTTP_ACCESS_CONTROL_REQUEST_HEADERS']))
            header("Access-Control-Allow-Headers:        {$_SERVER['HTTP_ACCESS_CONTROL_REQUEST_HEADERS']}");
    }
       $query = "UPDATE task_managment.workers SET name=" .$_POST['user']['Name'].",user_name='{user.UserName}',email={user.EMail},job='{user.JobId}',manager={user.ManagerId}WHERE worker_id={user.Id}";
    $resultObj = $connection->query($query);
    $users = array();
$CleintUser = array();
$i =0;
     while ($singleRowFromQuery = $resultObj->fetch_array(MYSQLI_ASSOC)) {
            $users[] = array('users' => $singleRowFromQuery);
            $CleintUser[$i] = new User($singleRowFromQuery);
            $i++;
        }
    header('Content-type: application/json');
      die(json_encode($CleintUser));
}

function connection() {

    global $connection;
    $dbPassword = null;
    $dbUserName = "root";
    $dbServer = "localhost";
    $dbName = "task_managment";
    $connection = new mysqli($dbServer, $dbUserName, $dbPassword, $dbName);
    if ($connection->connect_errno) {
        //echo "Database Connection Failed. Reason: ".$connection->connect_error;
        exit("Database Connection Failed. Reason: " . $connection->connect_error);
    }
}
        ?>
  