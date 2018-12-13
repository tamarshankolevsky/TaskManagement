use task_managment;
#---------------------------------------------
select * from users;

insert into users values('100', 'tamar', 'tamar', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'tamar5161718@gmail.com', 1, NULL, 1);
insert into users values('101', 'team', 'team', '4cc8f4d609b717356701c57a03e737e5ac8fe885da8c7163d3de47e01849c635', 'tamar5161718@gmail.com', '2', '100', '1');
insert into users values('102', 'worker', 'worker', '68487dc295052aa79c530e283ce698b8c6bb1b42ff0944252e1910dbecdc5425', 'tamar5161718@gmail.com', '3', '101', '1');
insert into users values('3', 'Rachel', 'Rachel', 'af41e68e1309fa29a5044cbdc36b90a3821d8807e68c7675a6c495112bc8a55f', 'tamar5161718@gmail.com', '4', '38', '1');
insert into users values('37', 'Lea', 'Lea', 'af41e68e1309fa29a5044cbdc36b90a3821d8807e68c7675a6c495112bc8a55f', 'tamar5161718@gmail.com', '3', '38', '0');
insert into users values('38', 'Rivka', 'Rivka', 'af41e68e1309fa29a5044cbdc36b90a3821d8807e68c7675a6c495112bc8a55f', 'tamar5161718@gmail.com', '2', NULL, '1');
insert into users values('103', 'Chava', 'Chava', '3315c3490373d0c9c0eaf58f14331452d0f3b6e4db131d106d83376a61858b77', 'chava@gmail.com', '3', '38', '0');
insert into users values('107', 'Chana', 'Chana', '8e5e368d32ec577a3c488cfc75b8caba618dd56000c75a19e350b57a721f0c46', 'chana@gmail.com', '2', '38', '1');
insert into users values('108', 'Ruth', 'Ruth', '8f1d6eefd399583c65107ad0d7cae2ef3bfd52fd098030d343f0aef53fc0b360', 'ruth@gmail.com', '4', '101', '1');
insert into users values('110', 'Efrat', 'Efrat', '68487dc295052aa79c530e283ce698b8c6bb1b42ff0944252e1910dbecdc5425', 'eee@gmail.com', '4', '101', '1');
insert into users values('111', 'Zvia', 'Zvia', '9d267ea203ac6efd37fcf6ef22b8d59923dc0b60d5184a9dc29a2d79d1a402bd', 'zvia@gmail.com', '5', '38', '1');
insert into users values('112', 'Tami', 'Tami', '661d182bd53a0e693ee1049357dbb7aa9a998b46e549bc524af0f6d2924136bb', 'tami@gmail.com', '4', '101', '1');
insert into users values('113', 'Yehudit', 'Yehudit', '95653cb4b66ab4cecfb93886d36a3e3dc830c634f613957bec751c36061c243f', 'eeeeee@gmail.com', '4', '107', '1');
insert into users values('114', 'Yael', 'Yael', '4cc8f4d609b717356701c57a03e737e5ac8fe885da8c7163d3de47e01849c635', 'tamar5161718@gmail.com', '2', NULL, '1');
insert into users values('115', 'Elisheva', 'Elisheva', '767f4a1d56bee7358576e006c8a9d17c7efd12a4357ea6d1fd530fafd587da41', 'tamar5161718@gmail.com', '5', '101', '1');
insert into users values('116', 'Sara', 'Sara', 'f970b8cd02181f2ee11ef139884223f3fb5eebd667004d6012a9d1f225184305', 'tamar@gmail.com', '4', '101', '1');
								
#---------------------------------------------
select * from projects;

# project_id, name, customer, team_leader, develop_houres, qa_houres, ui_ux_houres, start_date, end_date, is_complete
									
insert into projects values('1', 'SelTex', 'customerB', '101', '22', '10', '50', '2018-02-02 00:00:00', '2018-12-14 00:00:00', '1');
insert into projects values('2', 'selcom', 'customerA', '38', '300', '250', '100', '2018-02-02 00:00:00', '2019-07-07 00:00:00', '0');
insert into projects values('3', 'buildOnize', 'customerA', '38', '22', '12', '10', '2018-01-01 00:00:00', '2019-01-03 00:00:00', '0');
insert into projects values('4', 'WorkingOn', 'customerB', '101', '63', '52', '52', '2018-01-12 00:00:00', '2018-12-14 00:00:00', '0');
insert into projects values('5', 'TaskManagement', 'customerC', '101', '15', '15', '15', '2018-01-12 00:00:00', '2021-11-30 00:00:00', '0');
insert into projects values('6', 'BookStore', 'customerD', '101', '22', '12', '10', '2018-01-12 00:00:00', '2022-01-12 00:00:00', '0');
insert into projects values('7', 'projectA', 'customerA', '101', '22', '12', '10', '2018-07-12 00:00:00', '2019-01-12 00:00:00', '0');
insert into projects values('8', 'projectE', 'ADS', '38', '22', '12', '10', '2018-11-20 00:00:00', '2022-11-25 00:00:00', '0');
insert into projects values('9', 'projectF', 'customerD', '38', '22', '12', '10', '2018-11-14 00:00:00', '2018-12-14 00:00:00', '0');
insert into projects values('10', 'projectG', 'customerE', '38', '22', '10', '10', '2018-11-21 00:00:00', '2020-12-25 00:00:00', '0');
insert into projects values('11', 'newProject', 'customerA', '38', '22', '12', '10', '2018-11-29 00:00:00', '2019-11-30 00:00:00', '0');
insert into projects values('12', 'projectC', 'customerH', '101', '101', '12', '10', '2018-11-27 00:00:00', '2018-12-14 00:00:00', '0');
insert into projects values('13', 'projectB', 'customerA', '101', '22', '12', '10', '2018-11-23 00:00:00', '2022-11-30 00:00:00', '0');
insert into projects values('12', 'projectC', 'customerB', '101', '101', '12', '10', '2018-11-27 00:00:00', '2018-12-14 00:00:00', '0');
insert into projects values('13', 'projectB', 'customerD', '101', '22', '12', '10', '2018-11-23 00:00:00', '2018-12-14 00:00:00', '0');
#---------------------------------------------
select * from user_projects;

# user_project_id, user_id, project_id, allocated_hours
insert into user_projects values('4','37','2','20');
insert into user_projects values('6','3',	'1','0');
insert into user_projects values('7','3'	,'4','40');
insert into user_projects values('8','102','4'	,'2');
insert into user_projects values('9','3'	,'5'	,'1');
insert into user_projects values('10','102'	,'5',	'0');
insert into user_projects values('11','3',	'6','10'	);
insert into user_projects values('12','102',	'6'	,'10');
insert into user_projects values('13','3'	,'7',	'10');
insert into user_projects values('14','102',	'7','10');
insert into user_projects values('15','37'	,'8'	,'50');
insert into user_projects values('16','103','	8','50'	);
insert into user_projects values('17','107',	'8'	,'50');
insert into user_projects values('18',	'37','	9','50'	);
insert into user_projects values('19','103','9','10'	);
insert into user_projects values('20','107','9','10'	);
insert into user_projects values('21','37','10','10');
insert into user_projects values('22','103','10','10'	);
insert into user_projects values('23','107','10','10'	);
insert into user_projects values('24','37','11'	,'10');
insert into user_projects values('25','103','11'	,'10');
insert into user_projects values('26','107','11','10'	);
insert into user_projects values('55','38','31','0');
insert into user_projects values('56','101','31','0');
insert into user_projects values('57','	102','31','0');
insert into user_projects values('58','38'	,'32','0'	);
insert into user_projects values('59','101'	,'32'	,'0');
insert into user_projects values('60','102','32');
insert into user_projects values('61','116','1','10'	);
insert into user_projects values('62','115'	,'2','10');
insert into user_projects values('63'	,'108','3','10');
insert into user_projects values('64','110'	,'3'	,'0');
insert into user_projects values('65'	,'113','32','0'	);
insert into user_projects values('66'	,'115','	32'	,'10');
insert into user_projects values('67',	'112'	,'32'	,'10');
insert into user_projects values('68	','115','34','0'	);
insert into user_projects values('69'	,'116','34','0'	);


ALTER USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY '1234';