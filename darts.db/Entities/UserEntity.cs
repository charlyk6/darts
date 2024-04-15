﻿namespace darts.db.Entities
{
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// фамилия
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// имя
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// ник
        /// </summary>
        public string? NickName { get; set;}
    }
}