# Database Name:ChatBox

Table Name:Users
			UserId int AUTO_INCREMENT,
			UserName varchar(MAX) NOT NULL,
			PRIMARY KEY(UserId)
			
		
Table Name:ChatMessages
			ChatMessageId int AUTO_INCREMENT,
			UserId int,
			Message varchar(MAX),
			SendMessageTime datetime,
			primary key	(ChatMessageId),
			FOREIGN KEY(UserId) REFERENCES Users(UserId)

