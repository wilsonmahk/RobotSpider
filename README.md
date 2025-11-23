RobotSpider is a console application written in C# (.NET 8) that simulates navigating an autonomous robotic spider across a rectangular wall grid. The spider receives simple movement commands and reports its final position and direction.
This project includes a dedicated xUnit test project.

Accepts wall size (Grid) input, e.g., 7 15
Accepts spider starting position, e.g., 4 10 Left
Accepts movement instructions (F, L, R)

Validates:
Grid dimensions must be non-negative
Spider starting position must be inside grid
Instructions must contain only F, L, R
Spider cannot move outside the grid

Includes unit tests covering:
Parsing
Boundary enforcement
Invalid inputs
Command execution
Performance (large instruction sets)
