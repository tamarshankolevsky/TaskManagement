CREATE DATABASE  `task_managment`;

use `task_managment`;

#----------statuses----------
CREATE TABLE `statuses` (
  `status_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(15) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`status_id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

#----------users----------
CREATE TABLE `users` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(15) CHARACTER SET utf8 NOT NULL,
  `user_name` varchar(10) CHARACTER SET utf8 NOT NULL,
  `password` varchar(64) CHARACTER SET utf8 NOT NULL,
  `email` varchar(30) CHARACTER SET utf8 NOT NULL,
  `status` int(11) DEFAULT NULL,
  `manager` int(11) DEFAULT NULL,
  `is_active` int(11) DEFAULT '1',
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `user_name` (`user_name`),
  KEY `fk_status` (`status`),
  KEY `fk_manager` (`manager`),
  CONSTRAINT `fk_manager` FOREIGN KEY (`manager`) REFERENCES `users` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_status` FOREIGN KEY (`status`) REFERENCES `statuses` (`status_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=119 DEFAULT CHARSET=latin1;

#----------projects----------
CREATE TABLE `projects` (
  `project_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(15) CHARACTER SET utf8 NOT NULL,
  `customer` varchar(15) CHARACTER SET utf8 NOT NULL,
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
  CONSTRAINT `fk_team_leader` FOREIGN KEY (`team_leader`) REFERENCES `users` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=latin1;

#----------status permition----------
CREATE TABLE `status_permition` (
  `status_permition_id` int(11) NOT NULL AUTO_INCREMENT,
  `status_id` int(11) NOT NULL,
  `permition_id` int(11) NOT NULL,
  PRIMARY KEY (`status_permition_id`),
  KEY `fk_statuses` (`status_id`),
  KEY `fk_permition` (`permition_id`),
  CONSTRAINT `fk_permition` FOREIGN KEY (`permition_id`) REFERENCES `permition` (`permition_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_statuses` FOREIGN KEY (`status_id`) REFERENCES `statuses` (`status_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#----------user projects----------
CREATE TABLE `user_projects` (
  `user_project_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) NOT NULL,
  `project_id` int(11) NOT NULL,
  `allocated_hours` float DEFAULT NULL,
  PRIMARY KEY (`user_project_id`),
  KEY `fk_user` (`user_id`),
  KEY `fk_project` (`project_id`),
  CONSTRAINT `fk_project` FOREIGN KEY (`project_id`) REFERENCES `projects` (`project_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=55 DEFAULT CHARSET=latin1;

#----------daily presence----------
CREATE TABLE `daily_presence` (
  `daily_presence_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_project_id` int(11) NOT NULL,
  `date` date NOT NULL,
  `start` time DEFAULT NULL,
  `end` time DEFAULT NULL,
  PRIMARY KEY (`daily_presence_id`),
  KEY `fk_project_user` (`user_project_id`),
  CONSTRAINT `fk_project_user` FOREIGN KEY (`user_project_id`) REFERENCES `user_projects` (`user_project_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=210 DEFAULT CHARSET=latin1;


#----------insert----------
INSERT INTO task_managment.projects VALUES (0,'ProjectA','cA',1,300,250,100,'02/02/2018 00:00:00','07/07/2018 00:00:00');
INSERT INTO task_managment.projects VALUES (1,'ProjectB','cB',101,300,250,100,'02/05/2018 00:00:00','07/07/2020 00:00:00');

INSERT INTO users (user_id,name,user_name,password,email,status,manager) values (37,'worker','worker','68487dc295052aa79c530e283ce698b8c6bb1b42ff0944252e1910dbecdc5425','worker@gmail.com',3,101);
INSERT INTO users (user_id,name,user_name,password,email,status) values (1,'team','team','4cc8f4d609b717356701c57a03e737e5ac8fe885da8c7163d3de47e01849c635','teamleader@gmail.com',2);
INSERT INTO users (user_id,name,user_name,password,email,status) values (0,'tamar','tamar','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92"','tamar@gmail.com',1);
INSERT INTO users (user_id,name,user_name,password,email,status) values (5,'fff','fff','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92"','fffff@gmail.com',1);
INSERT INTO users (user_id,name,user_name,password,email,status) values (50,'manager','manager','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','manager@gmail.com',1);
INSERT INTO users (user_id,name,user_name,password,email,status) values (51,'manag','manag','68487dc295052aa79c530e283ce698b8c6bb1b42ff0944252e1910dbecdc5425','manager@gmail.com',1);
INSERT INTO users (user_id,name,user_name,password,email,status) values (40,'mmm','mmm','68487dc295052aa79c530e283ce698b8c6bb1b42ff0944252e1910dbecdc5425','mmmmmm@gmail.com',1);

INSERT INTO users (user_id,name,user_name,password,email,status,manager) values (39,'workerTw','workerTw','68487dc295052aa79c530e283ce698b8c6bb1b42ff0944252e1910dbecdc5425','worker@gmail.com',3,1);
INSERT INTO user_projects(user_id,project_id,allocated_hours) values (3,1,30);

INSERT INTO statuses ( name) values ('manager');
INSERT INTO statuses ( name) values ('teamLeader');
INSERT INTO statuses ( name) values ('developer');
INSERT INTO statuses ( name) values ('QA');
INSERT INTO statuses ( name) values ('UIUX');

#------------select------------
select * from users;
select * from projects;
select * from user_projects;
select * from daily_presence;
select * from statuses;

#----------update tables----------
ALTER TABLE `task_managment`.`users`
	ADD is_active INT DEFAULT 1 NULL;
    
delete from `task_managment`.`users` where user_name='tamar';

delete from `task_managment`.`daily_presence` where daily_presence_id=59;
 ALTER TABLE `task_managment`.`projects`
	ADD is_complete INT DEFAULT 0 NULL;  
    
    
    
    ALTER USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY '1234';