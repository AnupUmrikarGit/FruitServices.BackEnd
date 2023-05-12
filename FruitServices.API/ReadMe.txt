API Name: FruitService 
Framework: .net6 

Application has following four projects

1. FruitServices.API
   a. HealthController is implemented to check the Service is active or not.
   b. FruitController is implemented to receive the Requests, validate (if required)
      and then forward that request to Appliction Layer.  

2. FruitServices.Application
   a. This layer contains Business logic (if any), and forward the request to 
      infrastructure layer.

3. FruitServices.Infrastructure
   a. Infrastructure Layer calling fruityvice API to get the Fruit data.

4. FruitServices.Domain
   Contains entities.

5. Unit testing project.  
   implemented using xUnit framework

Pending Points:
1. Implement Identity server and add Authorization attribute in the FruitController.
2. Add more test cases.

