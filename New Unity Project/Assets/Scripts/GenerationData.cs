using System;
using System.Collections.Generic;
using System.Text;

namespace randomGeneration
{
    class GenerationData // Creates and Modifies a 2D array of integers
    {
        const int size = 200; // The width and height of the map

        int[,] data = new int[size, size]; // The data of the map
        int[,] tempData = new int[size, size]; // An array that can be used for calculations, preventing unwanted changes to the map

        Random random = new Random();

        public int[,] biomeForest() // Preset for the forest biome
        // 0 = Grass Path
        // 1 = Dirt Path
        // 2 = Tree Object
        // 3 = Bush Object
        // 4 = Forest Boundary
        {
            generateBorderData(5, 4); // Create Border
            generatePathData(1, 10, 20, 3, 1, 4, 1); // Create Dirt Path
            generatePlaceableData(2, 2); // Create Trees
            generatePlaceableData(1, 3); // Create Bushes
            return getGenerationData();
        }

        public int[,] biomeCave() // Preset for the cave biome
        // 0 = Stone Path
        // 1 = Torch Object
        // 2 = Stone Object
        // 3 = Ore Object
        // 4 = Stone Boundary
        {
            generateBorderData(10, 4); // Create Border
            generatePathData(5, 10, 20, 3, 3, 2, 1); // Create the floor
            fillEmpty(4); // Fill the empty tiles
            swapDataValue(1, 0); // Empty the nonborder tiles
            generatePlaceableData(7, 1); // Create torches
            generatePlaceableData(5, 2); // Create stones
            generatePlaceableData(3, 3); // Create ore
            return getGenerationData();
        }

        public int[,] biomeBeach() // Preset for the beach biome
        // 0 = Sand Path
        // 1 = Shallow Water Path
        // 2 = Shell Object
        // 3 = Ripples Object
        // 4 = Water Boundary
        {
            generateBorderData(15, 4); // Create Border
            generatePathData(2, 40, 20, 3, 0, 100, 5); // Create the sand
            generatePathData(1, 40, 20, 3, 2, 100, 2); // Create the shallow water
            fillEmpty(4); // Fill the empty tiles with deep water
            swapDataValue(2, 0); // Empty the sand
            generatePlaceableData(2, 3); // Create Ripples in the shallow water
            swapDataValue(0, 1); // Place the shallow water back
            swapDataValue(5, 0); // Empty the sand path
            generatePlaceableData(4, 2); // Create Shells on the sand
            return getGenerationData();
        }

        public int[,] biomeArctic() // Preset for the arctic biome
        // 0 = Snow Path
        // 1 = Torch Object
        // 2 = Ice Object
        // 3 = Stone Object
        // 4 = Ice Boundary
        {
            generateBorderData(15, 4); // Create Border
            generatePlaceableData(15, 1); // Create torches
            generatePlaceableData(10, 2); // Create ice
            generatePlaceableData(5, 3); // Create stone
            return getGenerationData();
        }

        public int[,] biomeVolcano() // Preset for the volcano biome
        // 0 = Rock Path
        // 1 = Ash Path
        // 2 = Fire Object
        // 3 = Bones Object
        // 4 = Lava Boundary
        {
            generatePathData(3, 10, 40, 3, 1, 3, 1); // Create the rock path
            fillEmpty(4); // Fill the remaining area with lava
            swapDataValue(1, 0); // Empty the rock path
            generatePlaceableData(3, 4); // Create random lava pools
            generatePathData(3, 10, 40, 3, 1, 3, 2); // Create ash path
            generatePlaceableData(3, 3); // Create bones in the ash
            fillEmpty(1); // Place the ash back
            swapDataValue(2, 0); // Empty the rock path
            generatePlaceableData(3, 2); // Create fire in rock path
            return getGenerationData();
        }

        public void displayColor(ConsoleColor c0, ConsoleColor c1, ConsoleColor c2, ConsoleColor c3, ConsoleColor c4) // Displays the data array to the console with color
        {
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (data[i, j] == 0) Console.ForegroundColor = c0;
                    else if (data[i, j] == 1) Console.ForegroundColor = c1;
                    else if (data[i, j] == 2) Console.ForegroundColor = c2;
                    else if (data[i, j] == 3) Console.ForegroundColor = c3;
                    else if (data[i, j] == 4) Console.ForegroundColor = c4;
                    Console.Write(0);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void displayData() // Displays the data array to the console
        {
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(data[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void displayTemp() // Displays the tempData array to the console
        {
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(tempData[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void fillEmpty(int value) // Replaces all values in data with a value of 0 with value
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (data[i, j] == 0) data[i, j] = value;
                }
            }
        }

        public int findDistance(int i1, int j1, int i2, int j2) // Returns the number of tiles between two locations (not diagonal)
        {
            return Math.Abs(i1 - i2) + Math.Abs(j1 - j2);
        }

        public void generateBorderData(int borderVariability, int tileValue)
        // This method generates a boundary along the edges of the grid
        // borderVariability: controls the maximum possible distance from the edges that the border can reach, higher values = bigger boundaries
        // tileValue: the value that is added to the data array
        {
            resetTempData();
            // Sets every tile on the border of tempData to a random number, with the range being increased by borderVariability
            for (int i = 0; i < size; i++)
            {
                tempData[i, 0] = random.Next(1, borderVariability + 1);
                tempData[i, size - 1] = random.Next(1, borderVariability + 1);
                tempData[0, i] = random.Next(1, borderVariability + 1);
                tempData[size - 1, i] = random.Next(1, borderVariability + 1);
            }
            // Goes through the entire tempData array, increasing values so that tiles are at most 1 away from their neighbors
            for (int k = 0; k < borderVariability + 1; k++)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        surroundingMin(i, j, tempData[i, j] - 1);
                    }
                }
            }
            // Adds the tileValue to data wherever tempData has a value > 0
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (tempData[i, j] != 0) data[i, j] = tileValue;
                }
            }
        }

        public void generatePathData(int pathSize, int positionCount, int pathDistance, int spreadValue, int decayThreshhold, int decayValue, int tileValue)
        // This method generates connected pathways or landmasses, with a large number of factors effecting the result
        // pathSize: determines the width of the paths and areas generated, the value is the number of tiles that a placed path will expand without spread and decay
        // positionCount: determines the number of areas generated, with 1 additional area in the center
        // pathDistance: determines the minimum distance between areas
        // spreadValue: determines the chance that when expanding the paths their value will not go down, 2 is the minimum for there to be a chance to spread further, and higher values can reduce the spread
        // decayThreshhold: determines the maximum values that can decay, higher values will effect more tiles
        // decayValue: determines the chance that a tile can decay, higher values decrease the chance that a tile will decay
        // tileValue: the value that is added to the data array
        {
            resetTempData();
            int[,] positionTracker = new int[positionCount + 1, 2]; // Keeps track of locations
            setTempArea(size / 2, size / 2, pathSize, pathSize); // Creates an area at the center of the map
            // Records the location of the center area
            positionTracker[0, 0] = size / 2;
            positionTracker[0, 1] = size / 2;
            int pathCounter = 1; // Keeps track of the number of areas that have been created
            // Tries to place areas until positionCount areas have been created
            while (pathCounter < positionCount + 1)
            {
                // Picks a random position
                int iTest = random.Next(pathDistance, size - pathDistance);
                int jTest = random.Next(pathDistance, size - pathDistance);
                bool validPosition = true;
                // Makes sure that the chosen location is at least a distance of pathDistance away from all of the existing locations
                for (int k = 0; k < pathCounter; k++)
                {
                    if (findDistance(iTest, jTest, positionTracker[k, 0], positionTracker[k, 1]) < pathDistance) validPosition = false;
                }
                if (validPosition)
                {
                    // Record the location of the area
                    positionTracker[pathCounter, 0] = iTest;
                    positionTracker[pathCounter, 1] = jTest;
                    // Create the area, the size determined by pathSize
                    setTempArea(iTest, jTest, pathSize, pathSize);
                    // Increment the counter
                    pathCounter++;
                }
            } // Repeats until positionCount additional areas have been placed
            // Draws pathways between the created areas
            for (int k = 0; k < positionCount; k++)
            {
                int destination = random.Next(k + 1, positionCount + 1); // Picks a loctation to make a path to
                // Finds the start and end positions, and determines the min and max values
                int iStart = positionTracker[k, 0];
                int jStart = positionTracker[k, 1];
                int iEnd = positionTracker[destination, 0];
                int jEnd = positionTracker[destination, 1];
                int iMin = iStart;
                int iMax = iStart;
                int jMin = jStart;
                int jMax = jStart;
                if (iStart > iEnd) iMin = iEnd;
                else iMax = iEnd;
                if (jStart > jEnd) jMin = jEnd;
                else jMax = jEnd;
                // Creates the path, starting with the horizonal portion, then the vertical portion
                for (int i = iMin; i <= iMax; i++)
                {
                    tempData[i, jStart] = pathSize;
                }
                for (int j = jMin; j <= jMax; j++)
                {
                    tempData[iEnd, j] = pathSize;
                }
            }
            // Expands the area the paths cover by making it so each tile tile is at most 1 away from its neighbors
            for (int k = 0; k < pathSize + 1; k++)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        surroundingMin(i, j, tempData[i, j] - random.Next(0, spreadValue)); // Increases the surrounding tiles, with the amount depending on spreadValue
                    }
                }
            }
            // Has a change to remove some of the tiles based decayThreshhold and decayValue
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (tempData[i, j] <= decayThreshhold && random.Next(0, decayValue) == 0) tempData[i, j] = 0;
                }
            }
            // Adds the tileValue to data wherever tempData has a value > 0
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (tempData[i, j] != 0 && data[i, j] == 0) data[i, j] = tileValue;
                }
            }
        }

        public void generatePlaceableData(int placementRadius, int tileValue)
        // This method generates randomly scattered objects until they can no longer be placed
        // placementRadius: the distance between the placed objects and nonempty tiles
        // tileValue: the value that is added to the data array
        {
            resetTempData();
            // Marks the location of the preexisting objects in data on tempData
            markTempData();
            // Marks all of the locations that are within placementRadius tiles of other marked tiles
            for (int k = 0; k < placementRadius; k++)
            {
                swapTempValue(-1, -2);
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (tempData[i, j] == -2) surroundingMax(i, j, -1);
                    }
                }
            }
            // Creates two empty lists that will be used to store and count the number of remaining locations for the object to be placed
            List<int> iPos = new List<int>();
            List<int> jPos = new List<int>();
            // Adds all of the possible location to place the object to the arrays
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (tempData[i, j] == 0)
                    {
                        iPos.Add(i);
                        jPos.Add(j);
                    }
                }
            }
            // Randomly adds objects to tempData until there are no more locations that they can be placed
            do
            {
                int rand = random.Next(0, iPos.Count); // Picks a random location
                setTempAreaReplace(iPos[rand], jPos[rand], placementRadius, -1, 0); // Marks all of the empty squares within placementRadius tiles of the location
                tempData[iPos[rand], jPos[rand]] = 1; // places an object at the location
                // Clears both lists
                iPos.Clear();
                jPos.Clear();
                // Adds all of the possible location to place the object to the arrays
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (tempData[i, j] == 0)
                        {
                            iPos.Add(i);
                            jPos.Add(j);
                        }
                    }
                }
            } while (iPos.Count > 0); // Stops when there are no more locations stored in the array
            // Adds the tileValue to data wherever tempData has a value > 0
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (tempData[i, j] == 1 && data[i, j] == 0) data[i, j] = tileValue;
                }
            }
        }

        public int[,] getGenerationData() // Returns the generation data
        {
            return data;
        }

        public void markTempData()
        // Sets every location where data != 0 to -1 in tempData
        // This leaves data unchanged
        // tempData is a copy of data, except every value is either 0 or -1
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (data[i, j] != 0) tempData[i, j] = -1;
                }
            }
        }

        // This takes too long at the moment, don't use
        public void removeUnreachable(int tileValue) // Removes all tiles that are unreachable from the center ^^^
        {
            resetTempData();
            markTempData();
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (tempData[i, j] == 0) count++;
                }
            }
            // Expand the starting tile to all reachable tiles
            if (tempData[size / 2, size / 2] == 0) tempData[size / 2, size / 2] = 1;
            for (int k = 0; k < count; k++)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (tempData[i, j] == 1) surroundingFlood(i, j, 1);
                    }
                }
            }
            // Replace each cell that could not be reached with a wall
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (tempData[i, j] == 0) data[i, j] = 4;
                }
            }
        }

        public void resetTempData() // Changes all of the values in tempData to 0
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    tempData[i, j] = 0;
                }
            }
        }

        public void setTempArea(int i, int j, int radius, int value)
        // Set tiles within radius tiles from position (i,j) in tempData to be value
        // (i,j): the center of the area in tempData
        // radius: how many tiles from the center are affected
        // the area is a square with a side length of (radius * 2 + 1), unless the dimensions would fall outside of the grid
        {
            // Sets the minimum and maximum i and j values that are affected
            int iMin = i - radius;
            int iMax = i + radius;
            int jMin = j - radius;
            int jMax = j + radius;
            // Changes any values that are outside of the array
            if (iMin < 0) iMin = 0;
            if (jMin < 0) jMin = 0;
            if (iMax >= size) iMax = size;
            if (jMax >= size) jMax = size;
            // Changes the values
            for (i = iMin; i <= iMax; i++)
            {
                for (j = jMin; j <= jMax; j++)
                {
                    tempData[i, j] = value;
                }
            }
        }

        public void setTempAreaReplace(int i, int j, int radius, int value, int replace)
        // Set tiles within radius tiles from position (i,j) in tempData to be value if they are replace
        // (i,j): the center of the area in tempData
        // radius: how many tiles from the center are possibly affected
        // replace: the value that is being replaced
        // the area is a square with a side length of (radius * 2 + 1), unless the dimensions would fall outside of the grid
        // every square in the area that has a value of (replace) becomes (value)
        {
            // Sets the minimum and maximum i and j values that can be affected
            int iMin = i - radius;
            int iMax = i + radius;
            int jMin = j - radius;
            int jMax = j + radius;
            // Changes any values that are outside of the array
            if (iMin < 0) iMin = 0;
            if (jMin < 0) jMin = 0;
            if (iMax >= size) iMax = size;
            if (jMax >= size) jMax = size;
            // Changes the values
            for (i = iMin; i <= iMax; i++)
            {
                for (j = jMin; j <= jMax; j++)
                {
                    if (tempData[i, j] == replace) tempData[i, j] = value;
                }
            }
        }

        public void surroundingFlood(int i, int j, int n) // Changes the 4 values surrounding the position (i,j) in tempData to n if they are 0
        {
            if (i != 0 && tempData[i - 1, j] == 0) tempData[i - 1, j] = n;
            if (i != size - 1 && tempData[i + 1, j] == 0) tempData[i + 1, j] = n;
            if (j != 0 && tempData[i, j - 1] == 0) tempData[i, j - 1] = n;
            if (j != size - 1 && tempData[i, j + 1] == 0) tempData[i, j + 1] = n;
        }

        public void surroundingMax(int i, int j, int n) // Changes the 4 values surrounding the position (i,j) in tempData to make them <= n
        {
            if (i != 0 && tempData[i - 1, j] > n) tempData[i - 1, j] = n;
            if (i != size - 1 && tempData[i + 1, j] > n) tempData[i + 1, j] = n;
            if (j != 0 && tempData[i, j - 1] > n) tempData[i, j - 1] = n;
            if (j != size - 1 && tempData[i, j + 1] > n) tempData[i, j + 1] = n;
        }

        public void surroundingMin(int i, int j, int n) // Changes the 4 values surrounding the position (i,j) in tempData to make them >= n
        {
            if (i != 0 && tempData[i - 1, j] < n) tempData[i - 1, j] = n;
            if (i != size - 1 && tempData[i + 1, j] < n) tempData[i + 1, j] = n;
            if (j != 0 && tempData[i, j - 1] < n) tempData[i, j - 1] = n;
            if (j != size - 1 && tempData[i, j + 1] < n) tempData[i, j + 1] = n;
        }

        public void swapDataValue(int originalValue, int newValue) // Changes every value in the data array that is originalValue to newValue
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (data[i, j] == originalValue) data[i, j] = newValue;
                }
            }
        }

        public void swapTempValue(int originalValue, int newValue) // Changes every value in the tempData array that is originalValue to newValue
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (tempData[i, j] == originalValue) tempData[i, j] = newValue;
                }
            }
        }
    }
}
