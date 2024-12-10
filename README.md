# PatientManagement API

This project is designed to manage patient data and includes both an API and a console application to generate test data. Below are the instructions on how to set up and run the project.

## 1. How to Run the Project

To set up the project, follow these steps:

1. Clone the repository:
   ```cmd
   git clone https://github.com/vshutchenko/PatientManagement.git
   ```
2. Navigate into the project directory:
   ```cmd
   cd PatientManagement
   ```
3. Start the project using Docker Compose:
   ```cmd
   docker-compose up
   ```

The API will be accessible on the following ports:
- **HTTP**: [http://localhost:8080/swagger](http://localhost:8080/swagger)
- **HTTPS**: [https://localhost:8443/swagger](https://localhost:8443/swagger)

## 2. How to Run the Console Application for Data Generation

To run the console application to generate test data, follow these steps:

1. Access the container running the console application:
   ```cmd
   docker exec -it console-app /bin/bash
   ```
2. Run the console application:
   ```cmd
   dotnet PatientManagement.ConsoleApp.dll
   ```

This will start the data generation process within the console application.


[Postman Collection for API Testing](./Patient%20API.postman_collection.json)

---