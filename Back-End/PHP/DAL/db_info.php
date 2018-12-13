<?php

$dbPassword = "1234";
$dbUserName = "root";
$dbServer = "127.0.0.1";
$dbName = "task_managment";

$connection = new mysqli($dbServer, $dbUserName, $dbPassword, $dbName);

if ($connection->connect_errno) {
    exit("Database Connection Failed. Reason: " . $connection->connect_error);
}

