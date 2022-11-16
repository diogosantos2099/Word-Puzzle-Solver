# Word-Puzzle-Solver
C# console app for solving the Word Puzzle problem

## The Problem
The program takes 3 arguments:
- dictionaryFile
- startWord
- endWord

Objective: from an already available dictionary of words, find the **shortest path** from startWord to endWord. 
In each transition, the current word must change only 1 character from the previous one.
Both words provided as input should respect the parameterized length in the app.
Examples:

	Input: spin spot
	Result: spin->spit->spot
	
	Input: same cost
	Result: same->came->case->cast->cost
	
	Input: kind spot
	Result: kind->find->fond->food->foot->soot->spot

## The Solution

### 'This is Sparta' algorithm
The idea behind this one is, much like a BFS (Breadth First Search), to explore every possibility on each iteration. We begin from the start word, find every word that matches it except by 1 character, and create possible paths based on that. This solution may not be the fastest, but it will always find a solution (if there is one). We are completely agnostic to the dictionary order, we do not rely on it in any way. We do not worry about falling into dead ends, because we never assume anything and explore all options. If a dead end occurs, that possible path is no longer considered, but we keep exploring all the other possible paths we know about. This ensures that if we find the end word in the current iteration, even if there is another path that could also lead there, it still wouldn't be shorter than the one we are on, so we have the solution.

Iteration process explained below. 

For each current path in possible paths:
	
    	1) Preemptively remove current path from possible paths. 
	Depending on the result of candidate words, 
    	we might add it back again multiple times, 
	with a new word at the end per word found (explained on step 3.c).
	
	2) For each letter in the current path’s last word,
	get all words from dictionary that match every character except one.
	Save all the results on a list of candidate words.
	This will give us all the possible next words relative to the current path’s last word.
	
	3) Evaluate the candidate words.
	3.a) If no candidate words exist, currentPath is a dead end. 
	Since we already removed it, nothing more to do. Back to 1).
	3.b) If candidate words exist, and one of them is endWord, 
	current path is the solution (at least one of). 
	We are done searching and can return already.
	3.c) Otherwise, exclude candidate words already present in current path.
	This leaves us with ‘n’ candidate words, which have never been used yet on current path, 
	so we add ‘n’ new paths to the possible paths, one per word found. 
	Each of these is based on current path, plus the new found word at the end of it. Back to 1).
Note: not finding any path is treated as an error.

## Benchmarks
### 'This is Sparta' algorithm
	
	Time taken: 0:00.005
	Solution:
	spin->spit->spot

	Time taken: 0:00.245
	Solution:
	same->came->case->cast->cost

	Time taken: 0:27.104
	Solution:
	kind->find->fond->food->foot->soot->spot
