# Task-Management-System
Task Management System

---------------------------------------------------------------------------------------------------------------------------------

Description: Console application which allows you to create, update and get list of tasks.

---------------------------------------------------------------------------------------------------------------------------------

Technologies used: .NET 6, Entity Framework Core, Protobuf (for serialization of messages before sending them to the queue), Spectre.Console (for a better user experience)
FYI: 
1) I haven't used any of the message brokers like Azure Serice Bus or RabbitMQ, in order to be able to run this console app locally, instead I used .NET delegates to imitate publisher/subscriber behavior
2) I used in-memory database for storing Tasks and MenuOptions

---------------------------------------------------------------------------------------------------------------------------------

User Flow:

1) Menu where user can select one of the options:
![image](https://github.com/dmytro-pos/Task-Management-System/assets/106164548/856d86a0-c71f-47d4-bb6a-07b1c8394eda)

- "Add new task" option selected: 

User asked to fill in questionarrie:
![image](https://github.com/dmytro-pos/Task-Management-System/assets/106164548/202ecacc-1fd2-493d-9d5d-e8f6ee7b4687)

And task is added to the in-memory database table.

- "Show list of tasks" option selected: 
![image](https://github.com/dmytro-pos/Task-Management-System/assets/106164548/79b045d9-a3f3-48ae-85bc-564b36a59ed1)

List of tasks is displayed to the user.

- "Update task status" option selected

User asked to fill in data related to task update request:
![image](https://github.com/dmytro-pos/Task-Management-System/assets/106164548/aafde4d4-e9a1-4eac-9d69-2a2c0de85f57)

Message: "Update request will be processed. In case of any invalid data - update will not be performed" displayed to user and request will try to be updated if data is valid.

![image](https://github.com/dmytro-pos/Task-Management-System/assets/106164548/a478f980-5942-42c0-b236-fe6cc07feb46)


---------------------------------------------------------------------------------------------------------------------------------

Implementation: 

I've created a separate ConsoleManager which is responsioble for all messages displayed to the user, so other classes are fully responsible for logic only.

On application startup all menu options which are enabled are loaded from the database and displayed to the user.

Once user selected one of the options we iterate through ITaskCommand[] array to find an appropriate command and once it's found ExecuteAsync method is called.

Regarding some of incompleted bonus points:
Didn't covered code with unit tests because of lack of time (nevertheless have of lot of experience in increasing code coverage and writting a lot of unit tests)
Didn't implemented retry logic as was not using any of message brokers, nevertheless I have an experience of implementation of circuit breaker pattern





