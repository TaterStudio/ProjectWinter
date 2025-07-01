namespace TaterGame.SnowIsland.Shared
{
    public static class Constraints
    {
        public static class Character
        {
            public const int SpeedScale = 100;
        }
        
        public static class Item
        {
            public const string Fist = "Fist";
        }
        public static class Input
        {
            public const string Move = "Move";
            public const string Jump = "Jump";
            public const string Attack = "Attack";
            public const string Look = "Look";
            public const string Sprint = "Sprint";
            public const string Crouch = "Crouch";
            public const string Interact = "Interact";
            public const string Aim = "Aim";
            public const string FakeDead = "FakeDead";
            public const string Radio = "Radio";
            public const string InvSlot1 = "Inv_Slot_1";
            public const string InvSlot2 = "Inv_Slot_2";
            public const string InvSlot3 = "Inv_Slot_3";
            public const string InvSlot4 = "Inv_Slot_4";
            public const string InvSlot5 = "Inv_Slot_5";
        }

        public static class Layer
        {
            public const string PlayerGhostLayer = "PlayerGhost";
        }
    }
}