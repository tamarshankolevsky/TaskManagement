<?php

//ROUTER
require './routes_loader.php';

//API
require './API/controllers/team_leader_controller.php';
require './API/controllers/report_controller.php';
require './API/controllers/project_controller.php';
require './API/controllers/hours_controller.php';
require './API/controllers/projectworker_controller.php';
require './API/controllers/user_controller.php';
require './API/controllers/status_controller.php';

//BLL
require './BLL/base_service.php';
require './BLL/user_service.php';
require './BLL/status_service.php';
require './BLL/hours_service.php';
require './BLL/report_service.php';
require './BLL/project_service.php';
require './BLL/team_leader_service.php';

//DAL
require './DAL/db_access.php';
require './DAL/db_info.php';

