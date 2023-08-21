Elevator Simulator

Steps: 
1. Provide nr of floors.
2. Provide floor numbers where elevators are stopped.
     For eg for input: 2,4,5 the application will create 3 elevators stopped at floors 2,4 and 5.
     The number of elevators is deducted.
	 The application assigns a color for each elevator for better console readibility
3. Enter the persons who wait for an elevator.
     For eg for input: 2,9 8,0 we have 2 persons: 
       First is waiting at floor 2 and wants to go to floor 9.
       Second, is waiting on floor 8 and wants to go to floor 0.
     After this command, an elevator is assigned for each person and elevators will start.
	 You can input this command while the elevators are moving.
	 
Video demonstration: 
![](https://github.com/StanculescuDaniel/Elevator/blob/main/ElevatorSimulator.gif)

Project structure:
- ElevatorSimulator.ConsoleApp - the console application responsible for getting the user input and for printing the elevator state
- ElevatorSimulator.Logic - contains the logic for the elevators. The logic is kept in a separate project so that it can be used in other types of applications.
- ElevatorSimulator.Logic.Abstractions - contains the models and interfaces
- ElevatorSimulator.Logic.Tests - contains the unit tests for ElevatorSimulator.Logic project.
