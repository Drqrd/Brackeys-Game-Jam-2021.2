<h1> Brackeys Game Jam 2021.2 </h1>
Author: Justin D'Errico & Shubham Tiwary <br>
Created: August, 2021 - August, 2021 <br>

<h2> Goal </h2>
The Brackeys Game Jam is a week long event in which aspiring game developers work and can collaborate with eachother in making a video game! My objective in the game jam was to learn about new topics, try and foster some creativity with the selected topic (Let there be chaos!), and most importantly, to 
 
<h2> Interesting Aspects </h2>
<h3> Finite State Machines </h3>
<h4> Problem </h4>
Implementing movement and other states for a  player in one script for all facets of a player leads to messy, indecipherable code.
<h4> Solution </h4>
Making use of a state machine, I was able to divide the many parts of a player script into modular sections which would transition to one another. This design is much more scalable than having everything in one script, as you can have multiple state machines running for different areas such as movement, combat, etc. <br>

<h2> Constraints </h2>
There were several facets of the game jam that inhibited the development of our game:
<h4> Time </h4>
This game jam actually ran pretty long, at 1 week's worth of time. Normally I spend several weeks developing my projects and so the introduction of a timer was definitely an important factor that limited what could be accomplished.
<h4> Time Zones </h4> 
A huge limiting factor in this game jam was my ability to communicate to my team member. Not only was this my first rodeo when it came to top down collaboration of game design, programming and implementation, but my team member lives in a time zone with a 10 hour difference from mine! This made the challenge of effective communication especially difficult, but making use of organizational tools helped in this regard: https://trello.com/b/MpX5bXZZ/brackeys-game-jam

<h2> Flaws </h2>
<h3> Design </h3>
The game has a flawed design in that all objects move towards the right. Initially this isn't a problem, but theoretically if the game goes on for long enough, the values for the positions of objects become too big to store and throw errors.
