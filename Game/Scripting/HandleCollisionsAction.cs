using System;
using System.Collections.Generic;
using System.Data;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when the snake 
    /// collides with the food, or the snake collides with its segments, or the game is over.
    /// </para>
    /// </summary>
    public class HandleCollisionsAction : Action
    {
        private bool isGameOver = false;
        private bool whichPlayerCollision = false;

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleCollisionsAction()
        {
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (isGameOver == false)
            {
                HandleCollisions(cast);
                HandleSegmentCollisions(cast);
                HandleGameOver(cast, whichPlayerCollision);
            }
        }

        /// <summary>
        /// Updates the score nd moves the food if the snake collides with it.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleCollisions(Cast cast)
        {
            Snake p1 = (Snake)cast.GetFirstActor("p1");
            Snake p2 = (Snake)cast.GetFirstActor("p2");
            
            //if (p1.GetHead().GetPosition().Equals(food.GetPosition()))
            //{
            //    int points = food.GetPoints();
            //    snake.GrowTail(points);
            //    score.AddPoints(points);
            //    food.Reset();
            //}
        }

        /// <summary>
        /// Sets the game over flag if the snake collides with one of its segments.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleSegmentCollisions(Cast cast)
        {
            Snake p1 = (Snake)cast.GetFirstActor("p1");
            Actor head1 = p1.GetHead();
            List<Actor> body1 = p1.GetBody();
            Snake p2 = (Snake)cast.GetFirstActor("p2");
            Actor head2 = p2.GetHead();
            List<Actor> body2 = p2.GetBody();

            foreach (Actor segment in body1)
            {
                if (segment.GetPosition().Equals(head1.GetPosition()))
                {
                    isGameOver = true;
                    whichPlayerCollision = false;
                }
            }
            foreach (Actor segment in body2)
            {
                if (segment.GetPosition().Equals(head2.GetPosition()))
                {
                    isGameOver = true;
                    whichPlayerCollision = true;
                }
            }
            foreach (Actor segment in body1)
            {
                if (segment.GetPosition().Equals(head2.GetPosition()))
                {
                    isGameOver = true;
                    whichPlayerCollision = true;
                }
            }
            foreach (Actor segment in body2)
            {
                if (segment.GetPosition().Equals(head1.GetPosition()))
                {
                    isGameOver = true;
                    whichPlayerCollision = false;
                }
            }
        }

        private void HandleGameOver(Cast cast, bool whichPlayerCollision)
        {
            if (isGameOver == true)
            {
                Snake p1 = (Snake)cast.GetFirstActor("p1");
                List<Actor> segments1 = p1.GetSegments();
                Snake p2 = (Snake)cast.GetFirstActor("p2");
                List<Actor> segments2 = p2.GetSegments();
                // create a "game over" message
                int x = Constants.MAX_X / 2;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);
                Actor message = new Actor();
                message.SetPosition(position);
                
                if (whichPlayerCollision == true)
                {
                    message.SetText("RED WINS!");
                }
                else
                {
                    message.SetText("YELLOW WINS!");
                }
                cast.AddActor("messages", message);

                // make everything white
                foreach (Actor segment in segments1)
                {
                    segment.SetColor(Constants.WHITE);
                }
                foreach (Actor segment in segments2)
                {
                    segment.SetColor(Constants.WHITE);
                }      
            }
        }

    }
}