namespace GoplayasiaBlazor.Models.Constants
{
    public class Settings
    {
        public class Constants
        {
            public const int ByteLimit = 5 * 1048576;

            public const string ClearVideoElement = "ClearVideoElement";
            public const string StopSignalRChecker = "StopSignalRChecker";
            public const string RestartIdleTimer = "RestartIdleTimer";
            public const string StopIdleTimer = "StopIdleTimer";
            public const string TabChange = "TabChange";
            public const string UpdateGameTokens = "UpdateGameTokens";
            public const string UpdateGameStatus = "UpdateGameStatus";
            public const string UpdateNotificationBadge = "UpdateNotificationBadge";
            public const string UpdateNotificationBadgeCount = "UpdateNotificationBadgeCount";
            public const string NotifyNewNotification = "NotifyNewNotification";
            public const string ImageSelectorTitle = "Open a Photo using?";
            public const string ImageSelectorGallery = "Gallery";
            public const string ImageSelectorCamera = "Camera";
            public const string ImageSelectorCancel = "NEVERMIND";
            public const string DirectToAgent = "Direct-to-Agent";
            public const string Bank = "Bank";
            public const string Gcash = "GCASH";
            public const string Cebuana = "Cebuana";
            //added by bong
            public const string PlayAntMedia = "PlayAntMedia";
            public const string StopAntMedia = "StopAntMedia";
            public const string PlayViewAntMedia = "PlayViewAntMedia";
            public const string StopViewAntMedia = "StopViewAntMedia";
            public const bool ForceAntMedia = false;
            //added by bong

            #region Lucky9
            public const string Draw = "DRAW";
            public const string Player = "PLAYER";
            public const string Banker = "BANKER";
            public const string FixedPlayer = "REGULAR-PLAYER";
            public const string FixedBanker = "REGULAR-BANKER";
            public const string RunningOddsPlayer = "RUNNING-PLAYER";
            public const string RunningOddsBanker = "RUNNING-BANKER";

            public const string UpdateDrawBetAmount = "UpdateDrawBetAmount";
            public const string UpdateFixed_Player_BetAmount = "UpdateFixed_Player_BetAmount";
            public const string UpdateFixed_Banker_BetAmount = "UpdateFixed_Banker_BetAmount";
            public const string UpdateRunningOdds_Player_BetAmount = "UpdateRunningOdds_Player_BetAmount";
            public const string UpdateRunningOdds_Banker_BetAmount = "UpdateRunningOdds_Banker_BetAmount";

            public const string TargetPlayer = "PLAYER-LUCKY9";
            public const string TargetBanker = "BANKER-LUCKY9";
            public const string SuitsPlayer = "PLAYER-ANY-SUITS";
            public const string SuitsBanker = "BANKER-ANY-SUITS";
            public const string ColorRedPlayer = "PLAYER-RED";
            public const string ColorBlackPlayer = "PLAYER-BLACK";
            public const string ColorRedBanker = "BANKER-RED";
            public const string ColorBlackBanker = "BANKER-BLACK";
            public const string PairPlayer = "PLAYER-ANY-PAIR";
            public const string PairBanker = "BANKER-ANY-PAIR";

            #endregion Lucky9

            #region First3 - Gold and Silver
            public const string Gold = "GOLD";
            public const string Silver = "SILVER";
            public const string FixedGold = "REGULAR-GOLD";
            public const string FixedSilver = "REGULAR-SILVER";
            public const string RunningOddsGold = "RUNNING-GOLD";
            public const string RunningOddsSilver = "RUNNING-SILVER";

            public const string UpdateDrawBetAmount_First3 = "UpdateDrawBetAmount_First3";
            public const string UpdateFixed_Gold_BetAmount = "UpdateFixed_Gold_BetAmount";
            public const string UpdateFixed_Silver_BetAmount = "UpdateFixed_Silver_BetAmount";
            public const string UpdateRunningOdds_Gold_BetAmount = "UpdateRunningOdds_Gold_BetAmount";
            public const string UpdateRunningOdds_Silver_BetAmount = "UpdateRunningOdds_Silver_BetAmount";


            public const string SuitsGold = "GOLD-ANY-SUITS";
            public const string SuitsSilver = "SILVER-ANY-SUITS";
            public const string ColorRedGold = "GOLD-RED";
            public const string ColorBlackGold = "GOLD-BLACK";
            public const string ColorRedSilver = "SILVER-RED";
            public const string ColorBlackSilver = "SILVER-BLACK";
            public const string Trio = "TRIO";
            #endregion First3 - Gold and Silver

            #region HEADS AND TAILS
            public const string Heads = "HEADS";
            public const string Tails = "TAILS";
            public const string FixedHeads = "REGULAR-HEADS";
            public const string FixedTails = "REGULAR-TAILS";
            public const string RunningOddsHeads = "RUNNING-HEADS";
            public const string RunningOddsTails = "RUNNING-TAILS";

            public const string UpdateDrawBetAmount_Heads_Tails = "UpdateDrawBetAmount_Heads_Tails";
            public const string UpdateFixed_Heads_BetAmount = "UpdateFixed_Heads_BetAmount";
            public const string UpdateFixed_Tails_BetAmount = "UpdateFixed_Tails_BetAmount";
            public const string UpdateRunningOdds_Heads_BetAmount = "UpdateRunningOdds_Heads_BetAmount";
            public const string UpdateRunningOdds_Tails_BetAmount = "UpdateRunningOdds_Tails_BetAmount";


            #endregion

            #region Go12
            public const string FixedBlack = "REGULAR-BLACK";
            public const string FixedRed = "REGULAR-RED";
            public const string Black = "BLACK";
            public const string Red = "RED";
            public const string One = "01";
            public const string Two = "02";
            public const string Three = "03";
            public const string Four = "04";
            public const string Five = "05";
            public const string Six = "06";
            public const string Seven = "07";
            public const string Eight = "08";
            public const string Nine = "09";
            public const string Ten = "10";
            public const string Eleven = "11";
            public const string Twelve = "12";

            public const string UpdateUserBet = "UpdateUserBet";
            public const string UpdateUserBetList = "UpdateUserBetList";
            #endregion Go12

            #region Giga Draw and Drop and Win
            public const string NormalPick = "NormalPick";
            public const string LuckyPick = "LuckyPick";
            public const string LuckyPickx3 = "LuckyPickx3";
            public const string LuckyPickx5 = "LuckyPickx5";
            public const string LuckyPickx10 = "LuckyPickx10";
            public const string UpdateGigaDrawBets = "UpdateGigaDrawBets";
            public const string UpdateDropAndWinBets = "UpdateDropAndWinBets";
            #endregion Giga Draw and Drop and Win

            #region DICE
            public const string DiceSmall = "DICE-SMALL";
            public const string DicecBig = "DICE-BIG";
            public const string DiceOdd = "DICE-ODD";
            public const string DiceEven = "DICE-EVEN";
            public const string FirstDice = "FIRST-DICE-";
            public const string SecondDice = "SECOND-DICE-";
            public const string ThirdDice = "THIRD-DICE-";
            public const string Odd = "ODD";
            public const string Even = "EVEN";
            public const string Small = "SMALL";
            public const string Big = "BIG";
            #endregion

            public const string UpdateGameOdds = "UpdateGameOdds";
            public const string UpdateGameTimer = "UpdateGameTimer";
            public const string UpdateEnableOperatorInput = "UpdateEnableOperatorInput";
            public const string UpdateEnableValidatorInput = "UpdateEnableValidatorInput";
            public const string UpdateBetValues = "UpdateBetValues";
            public const string UpdateGameWinners = "UpdateGameWinners";
            public const string NotifyGameRoundResult = "NotifyGameRoundResult";
            public const string SendBallGamesResult = "SendBallGamesResult";
            public const string UpdateTrends = "UpdateTrends";
            public const string UpdateEnableOpenButton = "UpdateEnableOpenButton";
            public const string NotifyFixedCancelled = "NotifyFixedCancelled";
            public const string NotifyOddsCancelled = "NotifyOddsCancelled";
            public const string NotifyFixedLeftOptions = "NotifyFixedLeftOptions";
            public const string NotifyFixedRightOptions = "NotifyFixedRightOptions";

            public const string NotifyNumberBets = "NotifyNumberBets";
            public const string NotifyNumberOptions = "NotifyNumberOptions";
            public const string NotifyEnableAllNumbers = "NotifyEnableAllNumbers";

            public const string NotifySignalRReconnection = "NotifySignalRReconnection";
            public const string NotifyForceLogout = "NotifyForceLogout";
            public const string SetNumberEntryFocus = "SetNumberEntryFocus";
            public const string ReenableSubmitButton = "ReenableSubmitButton";

            public const string Open = "OPEN";
            public const string Closed = "CLOSED";
            public const string Paused = "ON-HOLD";
            public const string Cancelled = "CANCELED";
            public const string StopTimer = "StopTimer";
            public const string StopVideo = "StopVideo";
            public const string PlayVideo = "PlayVideo";
            public const string CloseBettingPopups = "CloseBettingPopups";
            public const string DisConnectSignalR = "DisConnectSignalR";
            public const string ConnectSignalR = "ConnectSignalR";
            public const string UpdateAppActiveTime = "UpdateAppActiveTime";
            public const string IdleLogout = "IdleLogout";
            public const string AddToGameViewCount = "AddToGameViewCount";
            public const string RemoveFromGameViewCount = "RemoveFromGameViewCount";

            public const string DeviceOrientaionChanged = "DeviceOrientaionChanged";
            public const int Cashin = 1;
            public const int Cashout = 2;
            public const string UpdateCardResults = "UpdateCardResults";
            public const string UpdateBallGamesResult = "UpdateBallGamesResult";


            #region Game Colors
            public const string GameOpenColor = "background-color: blue;";
            public const string GameClosedColor = "background-color: red;";
            public const string GamePausedColor = "background-color: orange;";
            public const string GameCancelledColor = "background-color: gray;";
            #endregion

            #region StreamID
            public const string StreamIDLucky9 = "lucky9";
            public const string StreamIDFirs3 = "first3";
            public const string StreamIDGo12 = "go12";
            public const string StreamIDDrowin = "dropwin";
            public const string StreamIDGigadraw = "gigadraw";
            public const string StreamIDDrowinS5 = "dropwin";
            public const string StreamIDBigWin = "bigwin";
            public const string StreamIDLucky4 = "lucky4";
            public const string StreamIDHeadTails = "headtails";
            public const string StreamIDDice= "dice";
            public const string StreamIDBingo = "bingo";
            #endregion
            #region Reports
            public const string BetReturned = "Bet Returned";
            public const string PendingBets = "Pending";
            #endregion


        }

        #region ENUMS
        public enum PlayerCategory
        {
            Starter = 1,
            HighRoller,
            VIP
        }
        public enum L9MainBetTypes
        {
            Player = 1,
            Banker = 2,
            Draw = 3,
        }
        public enum L9SubBetTypes
        {
            Target9Player = 4,
            Target9Banker,
            AnypairPlayer,
            AnyPairBanker,
            SameSuitePlayer,
            SameSuiteBanker,
            TwoRedPlayer,
            TwoRedBanker,
            TwoBlackPlayer,
            TwoBlackBanker,
        }

        public enum F3MainBetTypes
        {
            Gold = 1,
            Silver = 2
        }
        public enum F3SubBetTypes
        {
            Trio = 3,
            SameSuiteGold,
            SameSuiteSilver,
            TwoRedGold,
            TwoRedSilver,
            TwoBlackGold,
            TwoBlackSilver,
        }

        public enum DiceBetTypes
        {
            AllDice = 1,
            Odd,
            Even,
            Small,
            Big,
            Single,
            FirstDice,
            SecondDice,
            ThirdDice,
            TripleDice,
            Number
        }

        public enum RoleTypes
        {
            Player = 1,
            Operator,
            MasterOperator,
            Validator,
            Agent,
            CorpMasterAgent,
            CorpAccount,
            CorpAgent,
            MasterAgent
        }

        public enum GameTypes
        {
            Go_12 = 1,
            Lucky_9,
            Gold_And_Silver,
            Heads_And_Tails,
            Giga_Draw,
            Drop_And_Win,
            Drop_And_WinS5,
            BigWin,
            Lucky4,
            Dice,
            Bingo
        }

        public enum BetStatus
        {
            Lose = 1,
            Win,
            Draw,
            Cancel,
            Consolation,
            ConsolationButNoPrize
        }

        public enum RoundStatus
        {
            Open = 1,
            Closed,
            Paused,
            Cancelled,
            PendingResult
        }

        public enum AddressType
        {
            Permanent = 1,
            Current
        }

        public enum ImageTypes
        {
            Profile = 1,
            GovernmentID
        }

        public enum WithDrawOptions
        {
            DirectToAgent = 1,
            Bank,
            Gcash,
            Cebuana
        }

        public enum NonCardGameBetTypes
        {
            Normal = 1,
            LuckyPick,
            LuckyPickx5,
            LuckyPickx10,
            LuckyPickx3,
        }

        public enum DropAndWinMainBetOption
        {
            F2 = 1,
            F3,
            All4
        }
        public enum Lucky4MainBetOption
        {
            F2 = 1,
            F3,
            All4
        }
        public enum Lucky4ChipTypes
        {
            F2 = 2,
            F3,
            All4
        }

        public enum TopupTypes
        {
            AGGREGATOR = 1,
            DIRECTTODISTRIBUTOR
        }

        public enum WithdrawTypes
        {
            CEBUANA = 1,
            DIRECTTODISTRIBUTOR
        }

        public enum TransactionSelectionTypes
        {
            OverTheCounter = 1,
            PaymentChannel,
            GoPlayAsia
        }

        public enum WithdrawPaymentChannelTypes
        {
            Cebuana = 1,
            GCash,
            Bank,
            UB,
            UBInstapay
        }

        public enum NotificationTypes
        {
            Won = 1,
            Lost,
            TokenRequest,
            WithdrawRequest
        }

        public enum ConfirmPopupType
        {
            Deactivate = 1,
            Logout,
            ForceLogout
        }

        public enum GoPlayAsiaChannel
        {
            UB = 1,
            GCash
        }

        public enum GameVariant
        {
            L9Target = 1,
            L9Suits,
            L9Color,
            L9Pair,
            F3Suits,
            F3Color,
            F3Trio,
            L9Draw,
            BW3Conso,
            BW2Conso,
            BW1Conso,
            G12Reg,
            DiceOddEven,
            DiceSmallBig,
            DiceSingle,
            DiceNumber,
            Bingo6Card
        }
        #endregion

        #region bingo constants
        public class Card
        {
            public int? Number { get; set; }
            public string Suit { get; set; }
            public string Value { get; set; }
            public string FinalResult { get; set; }

            public Card(int? number, string suit, string value, string finalResult)
            {
                Number = number;
                Suit = suit;
                Value = value;
                FinalResult = finalResult;
            }
        }
        public class BingoPermutation
        {
            public List<Card> BingoCards;
            public BingoPermutation()
            {
                BingoCards = new List<Card>
                    {
                    new Card(1, "d", "a", "d-a"),
                    new Card(2, "d", "2", "d-2"),
                    new Card(3, "d", "3", "d-3"),
                    new Card(4, "d", "4", "d-4"),
                    new Card(5, "d", "5", "d-5"),
                    new Card(6, "d", "6", "d-6"),
                    new Card(7, "d", "7", "d-7"),
                    new Card(8, "d", "8", "d-8"),
                    new Card(9, "d", "9", "d-9"),
                    new Card(10, "d", "10", "d-10"),
                    new Card(11, "d", "j", "d-j"),
                    new Card(12, "d", "q", "d-q"),
                    new Card(13, "d", "k", "d-k"),
                    new Card(14, "s", "a", "s-a"),
                    new Card(15, "s", "2", "s-2"),
                    new Card(16, "s", "3", "s-3"),
                    new Card(17, "s", "4", "s-4"),
                    new Card(18, "s", "5", "s-5"),
                    new Card(19, "s", "6", "s-6"),
                    new Card(20, "s", "7", "s-7"),
                    new Card(21, "s", "8", "s-8"),
                    new Card(22, "s", "9", "s-9"),
                    new Card(23, "s", "10", "s-10"),
                    new Card(24, "s", "j", "s-j"),
                    new Card(25, "s", "q", "s-q"),
                    new Card(26, "s", "k", "s-k"),
                    new Card(27, "h", "a", "h-a"),
                    new Card(28, "h", "2", "h-2"),
                    new Card(29, "h", "3", "h-3"),
                    new Card(30, "h", "4", "h-4"),
                    new Card(31, "h", "5", "h-5"),
                    new Card(32, "h", "6", "h-6"),
                    new Card(33, "h", "7", "h-7"),
                    new Card(34, "h", "8", "h-8"),
                    new Card(35, "h", "9", "h-9"),
                    new Card(36, "h", "10", "h-10"),
                    new Card(37, "h", "j", "h-j"),
                    new Card(38, "h", "q", "h-q"),
                    new Card(39, "h", "k", "h-k"),
                    new Card(40, "c", "a", "c-a"),
                    new Card(41, "c", "2", "c-2"),
                    new Card(42, "c", "3", "c-3"),
                    new Card(43, "c", "4", "c-4"),
                    new Card(44, "c", "5", "c-5"),
                    new Card(45, "c", "6", "c-6"),
                    new Card(46, "c", "7", "c-7"),
                    new Card(47, "c", "8", "c-8"),
                    new Card(48, "c", "9", "c-9"),
                    new Card(49, "c", "10", "c-10"),
                    new Card(50, "c", "j", "c-j"),
                    new Card(51, "c", "q", "c-q"),
                    new Card(52, "c", "k", "c-k")};
            }

        };

        #endregion
    }

}
