CREATE DATABASE  `task_managment`;

use `task_managment`;

#-------------------statuses-------------------
CREATE TABLE `statuses` (
  `status_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(15) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`status_id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

#--------------------users--------------------
CREATE TABLE `users` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `user_name` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `password` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `email` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `status` int(11) DEFAULT NULL,
  `manager` int(11) DEFAULT NULL,
  `is_active` int(11) DEFAULT '1',
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `user_name` (`user_name`),
  KEY `fk_status` (`status`),
  KEY `fk_manager` (`manager`),
  CONSTRAINT `fk_manager` FOREIGN KEY (`manager`) REFERENCES `users` (`user_id`),
  CONSTRAINT `fk_status` FOREIGN KEY (`status`) REFERENCES `statuses` (`status_id`)
) ENGINE=InnoDB AUTO_INCREMENT=171 DEFAULT CHARSET=latin1;

#-------------------projects-------------------
CREATE TABLE `projects` (
  `project_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `customer` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `team_leader` int(11) NOT NULL,
  `develop_houres` int(11) NOT NULL,
  `qa_houres` int(11) NOT NULL,
  `ui_ux_houres` int(11) NOT NULL,
  `start_date` datetime NOT NULL,
  `end_date` datetime NOT NULL,
  `is_complete` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`project_id`),
  UNIQUE KEY `name` (`name`),
  KEY `fk_team_leader` (`team_leader`),
  CONSTRAINT `fk_team_leader` FOREIGN KEY (`team_leader`) REFERENCES `users` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=latin1;


#-----------------user projects-----------------
CREATE TABLE `user_projects` (
  `user_project_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) NOT NULL,
  `project_id` int(11) NOT NULL,
  `allocated_hours` float DEFAULT NULL,
  PRIMARY KEY (`user_project_id`),
  KEY `fk_user` (`user_id`),
  KEY `fk_project` (`project_id`),
  CONSTRAINT `fk_project` FOREIGN KEY (`project_id`) REFERENCES `projects` (`project_id`),
  CONSTRAINT `fk_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=66 DEFAULT CHARSET=latin1;

#-----------------daily presence-----------------
CREATE TABLE `daily_presence` (
  `daily_presence_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_project_id` int(11) NOT NULL,
  `date` date NOT NULL,
  `start` time DEFAULT NULL,
  `end` time DEFAULT NULL,
  PRIMARY KEY (`daily_presence_id`),
  KEY `fk_project_user` (`user_project_id`),
  CONSTRAINT `fk_project_user` FOREIGN KEY (`user_project_id`) REFERENCES `user_projects` (`user_project_id`)
) ENGINE=InnoDB AUTO_INCREMENT=226 DEFAULT CHARSET=latin1;

