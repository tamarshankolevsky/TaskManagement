<?php

$dbPassword = "1234";
$dbUserName = "root";
$dbServer = "localhost";
$dbName = "task_managment";

$connection = new mysqli($dbServer, $dbUserName, $dbPassword, $dbName);

if ($connection->connect_errno) {
    exit("Database connection failed. reason: " . $connection->connect_errno);
}

function add($query)
{
   $connection->query($query); 
}




