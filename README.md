A simple project using the Minimal APIs implementing Layered Architecture, Repository Pattern with PostgreSQL, Docker, Docker Compose, Exception Handling with Logging using Serilog.
  1.	Clone the repository
  2.	Once Docker for Windows is installed, go to the Settings > Advanced option, from the Docker icon in the system tray, to configure the minimum amount of memory and CPU like so: • Memory: 4 GB • CPU: 2
  3.	At the root directory which include docker-compose.yml files, run below command: docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
  4.	Wait for docker compose all microservices. That’s it! (some microservices need extra time to work so please wait if not worked in first shut)
