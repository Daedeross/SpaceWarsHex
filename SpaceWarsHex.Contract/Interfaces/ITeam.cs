using System.Collections.Generic;

namespace SpaceWars.Interfaces
{
    /// <summary>
    /// Interface for a team (i.e. one side of a battle).
    /// </summary>
    public interface ITeam
    {
        /// <summary>
        /// Index of the team in the battle.
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// Name of the team/faction
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// List of players
        /// </summary>
        IReadOnlyList<IPlayer> Players { get; }

        /// <summary>
        /// All ships controlled by the player.
        /// </summary>
        IReadOnlyList<IShip> Ships { get; }

        /// <summary>
        /// All entities controlled by the player that can be selected.
        /// </summary>
        IReadOnlyCollection<ISelectable> Selectables { get; }

        /// <summary>
        /// All entities controlled/owned by the player.
        /// </summary>
        IReadOnlyCollection<IHexObject> ControlledEntities { get; }

        /// <summary>
        /// Adds an entity to the team. Must already be assitned to a Player on the team.
        /// </summary>
        /// <param name="entity">The Entity to add.</param>
        /// <returns>self, for fluent method-chaining.</returns>
        ITeam AddEntity(IHexObject entity);

        /// <summary>
        /// Removes an entity to the team. Must already be assitned to a Player on the team.
        /// </summary>
        /// <param name="entity">The Entity to remove.</param>
        /// <returns>self, for fluent method-chaining.</returns>
        ITeam RemoveEntity(IHexObject entity);

        /// <summary>
        /// Adds a player and all their entities
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>self, for fluent method-chaining.</returns>
        ITeam AddPlayer(IPlayer player);

        /// <summary>
        /// Removes a player and all their entities
        /// </summary>
        /// <param name="player">The player to remove.</param>
        /// <returns>self, for fluent method-chaining.</returns>
        ITeam RemovePlayer(IPlayer player);
    }
}
