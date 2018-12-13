<?php

require './includes.php';

header("Access-Control-Allow-Origin: *");
//header('Content-type: application/json');
header('Access-Control-Allow-Methods', 'GET, POST, PUT, DELETE, 
OPTIONS');
header("Access-Control-Allow-Headers: Origin, X-Requested-With, Content-Type, Accept");

$routes_loader = new routes_loader();

$link = "http://$_SERVER[HTTP_HOST]$_SERVER[REQUEST_URI]";
$path = parse_url($link, PHP_URL_PATH);
$exploded_path = explode('/', $path);
$controller_name=$exploded_path[count($exploded_path) - 2];
$method_name=$exploded_path[count($exploded_path) - 1];

date_default_timezone_set('Asia/Jerusalem');


$type=$_SERVER['REQUEST_METHOD'];

if($type == 'GET')
    $params=$_GET;
else if($type=='POST')
{
    $json = file_get_contents('php://input');
    $params = json_decode($json, true);
}
 else 
     $params =$_PUT;
//echo $params['userName'].'pppp';
echo $routes_loader->invoke($controller_name,$method_name,$params);

die();

