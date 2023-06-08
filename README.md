# myNETCoreAPI
Demo WinForms application in .NET Core 2.1 that demonstrates all the basic principiples of a REST API application coded in C# language using object oriented design and fundamentals.

# Database
Used a local SQL Express database for demo purposes.

# Overview
  The application demostrates examples of all basic endpoint of a REST API. 
    - GET Cities/Points of Interest (for retrieving multiple resources)
    - GET City/Point of Interest (to retrieve a single resource)
    - POST City/Point of Interest (to add/create and persist a single resource)
    - PUT City/Point of Interest (to update and persist a specific resource)
    - PATCH City/Point of Interest (to update a specific field in the resource)
    - DELETE City/Point of Interest (to delete/remote a resource)
    
# The App demostrates these Object Oriented design principles:

  - Use of Generics.
  - Single responsability classes. (classes have one responsability task)
  - Open closed principle. Classes can be extended or derived but not modified.
  - Use of interfaces.
  - Use of dependency injection.
  - Use of Object Relational Mapping (ORM) .NET Framework for sqllite
  - Error handling.
  - Simple logging via NLog platform.
