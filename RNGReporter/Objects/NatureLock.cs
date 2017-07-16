using System.Collections.Generic;

namespace RNGReporter.Objects
{
    public class NatureLock
    {
        private LockInfo[] lockInfo;
        private int forwardCounter, backCount, frontCount, shadowCount, x;
        private uint pid, pidOriginal, gender, genderLower, genderUpper, nature, psv, psvtemp;
        private bool sister;
        private ShadowType type;
        public List<uint> rand;
        private XdRngR reverse;
        private XdRng forward;

        public NatureLock(int lockNum)
        {
            lockInfo = natureLockList(lockNum);
            rand = new List<uint>();
            if (lockInfo != null)
            {
                backCount = lockInfo.Length;
                frontCount = backCount == 1 ? 0 : backCount - 2;
                shadowCount = backCount - 1;
                x = 0;
                if (backCount == 1)
                    getCurrLock();
            }
            reverse = new XdRngR(0);
            forward = new XdRng(0);
        }

        public void changeLock(int lockNum)
        {
            lockInfo = natureLockList(lockNum);
            if (lockInfo != null)
            {
                backCount = lockInfo.Length;
                frontCount = backCount == 1 ? 0 : backCount - 2;
                shadowCount = backCount - 1;
                x = 0;
                if (backCount == 1)
                    getCurrLock();
            }
        }

        private LockInfo[] natureLockList(int natureLockIndex)
        {
            switch (natureLockIndex)
            {
                case 0: //Altaria
                    type = ShadowType.SecondShadow;
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(0, 0, 126), new LockInfo(12, 127, 255) };
                case 1: //Arbok
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(12, 0, 126), new LockInfo(0, 0, 126), new LockInfo(6, 127, 255) };
                case 5: //Baltoy 2
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(0, 127, 255), new LockInfo(24, 127, 255) };
                case 6: //Banette
                    type = ShadowType.SecondShadow;
                    return new LockInfo[] { new LockInfo(12, 0, 255), new LockInfo(18, 0, 126), new LockInfo(0, 0, 255) };
                case 8: //Butterfree
                    type = ShadowType.SecondShadow;
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(6, 127, 255), new LockInfo(12, 0, 190) };
                case 10: //Chansey
                    type = ShadowType.SecondShadow;
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(6, 0, 126) };
                case 11: //Delcatty
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(0, 127, 255), new LockInfo(6, 0, 190) };
                case 12: //Dodrio
                    type = ShadowType.SingleLock;
                    return new LockInfo[] { new LockInfo(18, 0, 126) };
                case 13: //Dragonite
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(0, 127, 255), new LockInfo(12, 0, 126), new LockInfo(12, 0, 126), new LockInfo(18, 127, 255), new LockInfo(0, 127, 255) };
                case 14: //Dugtrio
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(12, 127, 255), new LockInfo(6, 0, 126), new LockInfo(18, 127, 255), new LockInfo(0, 127, 255) };
                case 15: //Duskull
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(18, 0, 126), new LockInfo(12, 127, 255) };
                case 16: //Electabuzz
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 0, 126), new LockInfo(24, 63, 255) };
                case 18: //Farfetch'd  
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(0, 0, 126), new LockInfo(12, 127, 255) };
                case 19: //Golduck
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 0, 126), new LockInfo(24, 127, 255) };
                case 20: //Grimer
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(12, 127, 255) };
                case 21: //Growlithe
                    type = ShadowType.SecondShadow;
                    return new LockInfo[] { new LockInfo(6, 0, 126), new LockInfo(24, 127, 255) };
                case 22: //Gulpin 3
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(6, 127, 255), new LockInfo(12, 0, 126) };
                case 23: //Gulpin 1
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(6, 127, 255), new LockInfo(12, 0, 126) };
                case 24: //Gulpin 2
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(0, 0, 126), new LockInfo(6, 127, 255), new LockInfo(12, 0, 126) };
                case 25: //Hitmonchan
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 0, 126), new LockInfo(24, 127, 255) };
                case 26: //Hitmonlee
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(24, 0, 126), new LockInfo(6, 0, 255), new LockInfo(12, 0, 126), new LockInfo(18, 127, 255) };
                case 30: //Hypno
                    type = ShadowType.SecondShadow;
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(6, 0, 126), new LockInfo(12, 0, 126), new LockInfo(18, 0, 126) };
                case 31: //Kangaskhan
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(12, 0, 255), new LockInfo(18, 0, 126), new LockInfo(0, 0, 255) };
                case 32: //Lapras
                    type = ShadowType.SecondShadow;
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(500, 500, 500), new LockInfo(500, 500, 500), new LockInfo(6, 0, 126) };
                case 33: //Ledyba
                    type = ShadowType.SingleLock;
                    return new LockInfo[] { new LockInfo(0, 0, 126) };
                case 34: //Lickitung
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(6, 0, 255), new LockInfo(24, 127, 255) };
                case 36: //Lunatone
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(0, 0, 126) };
                case 37: //Marcargo
                    type = ShadowType.SecondShadow;
                    return new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(6, 127, 255), new LockInfo(24, 127, 255) };
                case 38: //Magmar
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(18, 191, 255), new LockInfo(18, 127, 255) };
                case 39: //Magneton
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(0, 127, 255), new LockInfo(18, 0, 255) };
                case 40: //Makuhita
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 127, 255) };
                case 41: //Makuhita Colo
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(24, 127, 255) };
                case 42: //Manectric
                    type = ShadowType.SingleLock;
                    return new LockInfo[] { new LockInfo(6, 0, 126) };
                case 44: //Mareep 1
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(24, 127, 255) };
                case 45: //Mareep 2
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(0, 0, 255), new LockInfo(12, 0, 126), new LockInfo(24, 127, 255) };
                case 46: //Marowak
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(500, 500, 500), new LockInfo(500, 500, 500), new LockInfo(6, 0, 126) };
                case 47: //Mawile
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 127, 255) };
                case 48: //Meowth
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(0, 0, 126), new LockInfo(6, 63, 255) };
                case 50: //Mr. Mime
                    type = ShadowType.SecondShadow;
                    return new LockInfo[] { new LockInfo(6, 0, 126), new LockInfo(24, 127, 255), new LockInfo(18, 127, 255), new LockInfo(18, 127, 255) };
                case 51: //Natu
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(24, 127, 255) };
                case 52: //Nosepass
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(18, 127, 255), new LockInfo(0, 127, 255) };
                case 53: //Numel
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(24, 0, 126), new LockInfo(0, 0, 255), new LockInfo(6, 127, 255) };
                case 54: //Paras
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(6, 0, 126), new LockInfo(24, 127, 255) };
                case 55: //Pidgeotto
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(18, 32, 255), new LockInfo(12, 127, 255) };
                case 56: //Pineco
                    type = ShadowType.SingleLock;
                    return new LockInfo[] { new LockInfo(6, 127, 255) };
                case 57: //Pinsir
                    type = ShadowType.SecondShadow;
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(18, 191, 255), new LockInfo(18, 127, 255) };
                case 58: //Poliwrath
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(6, 0, 126), new LockInfo(24, 127, 255), new LockInfo(18, 127, 255), new LockInfo(18, 127, 255) };
                case 59: //Poochyena
                    type = ShadowType.SingleLock;
                    return new LockInfo[] { new LockInfo(12, 0, 126) };
                case 60: //Primeape
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(6, 0, 126), new LockInfo(12, 0, 126), new LockInfo(18, 0, 126) };
                case 61: //Ralts
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(6, 0, 126), new LockInfo(0, 63, 255) };
                case 62: //Rapidash
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(6, 127, 255), new LockInfo(24, 127, 255) };
                case 63: //Raticate
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(500, 500, 500), new LockInfo(18, 0, 126) };
                case 65: //Roselia
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(6, 127, 255) };
                case 66: //Sableye
                    type = ShadowType.SecondShadow;
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 0, 126), new LockInfo(24, 127, 255) };
                case 67: //Salamence
                    type = ShadowType.Salamence;
                    return new LockInfo[] { new LockInfo(6, 0, 126) };
                case 68: //Scyther
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(6, 0, 126) };
                case 70: //Seedot 1
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(12, 127, 255), new LockInfo(0, 127, 255), new LockInfo(18, 0, 126), new LockInfo(24, 0, 126), new LockInfo(6, 127, 255) };
                case 71: //Seedot 2
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(6, 127, 255), new LockInfo(0, 0, 126), new LockInfo(0, 0, 126), new LockInfo(24, 0, 126), new LockInfo(6, 127, 255) };
                case 72: //Seel
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(12, 127, 255), new LockInfo(6, 127, 255) };
                case 74: //Shroomish
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(24, 0, 126) };
                case 75: //Snorlax
                    type = ShadowType.SecondShadow;
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 0, 126), new LockInfo(24, 63, 255) };
                case 76: //Snorunt
                    type = ShadowType.SingleLock;
                    return new LockInfo[] { new LockInfo(6, 0, 126) };
                case 77: //Solrock
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(6, 127, 255), new LockInfo(24, 0, 255) };
                case 78: //Spearow
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(6, 0, 126), new LockInfo(18, 127, 255) };
                case 79: //Spheal 3
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(0, 0, 255), new LockInfo(12, 0, 126), new LockInfo(24, 127, 255) };
                case 80: //Spheal 1
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(24, 127, 255) };
                case 81: //Spheal 2
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(0, 0, 255), new LockInfo(12, 0, 126), new LockInfo(24, 127, 255) };
                case 82: //Spinarak
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(6, 127, 255), new LockInfo(12, 0, 126) };
                case 83: //Starmie
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(500, 500, 500), new LockInfo(0, 0, 126), new LockInfo(6, 127, 255), new LockInfo(24, 0, 255) };
                case 85: //Swinub
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(0, 127, 255), new LockInfo(18, 0, 126) };
                case 86: //Tangela
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(6, 127, 255), new LockInfo(12, 0, 190) };
                case 90: //Venomoth
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(12, 127, 255), new LockInfo(24, 0, 255), new LockInfo(18, 0, 126) };
                case 91: //Voltorb
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(12, 127, 255), new LockInfo(0, 127, 255) };
                case 92: //Vulpix
                    type = ShadowType.FirstShadow;
                    return new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(6, 0, 126), new LockInfo(0, 127, 255) };
                case 93: //Weepinbell
                    type = ShadowType.SecondShadow;
                    return new LockInfo[] { new LockInfo(12, 127, 255), new LockInfo(24, 0, 255), new LockInfo(18, 0, 126) };
                case 2: //Articuno 
                case 3: //Baltoy 3
                case 4: //Baltoy 1
                case 7: //Beedrill
                case 9: //Carvanha
                case 17: //Exeggutor
                case 27: //Houndour 3
                case 28: //Houndour 1
                case 29: //To do houndour 2
                case 35: //Lugia
                case 43: //Mareep 3
                case 49: //Moltres
                case 64: //Rhydon
                case 69: //To do seedot 3
                case 73: //Shellder
                case 84: //Swellow
                case 87: //Tauros
                case 88: //Teddiursa
                case 89: //Togepi
                case 94: //Zangoose
                default: //Zapdos 
                    type = ShadowType.NoLock;
                    return null;
            }
        }

        public bool method1SingleNL(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber();

            //Build PID
            pid = getPIDReverse();

            //Backwards nature lock check
            gender = pid & 255;
            return (gender >= genderLower && gender <= genderUpper && pid % 25 == nature);
        }

        public bool salamenceUnset(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(8);

            //Build PID
            pid = getPIDReverse();

            //Backwards nature lock check
            gender = pid & 255;
            return (gender >= genderLower && gender <= genderUpper && pid % 25 == nature);
        }

        public bool salamenceSet(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(6);

            //Build PID
            pid = getPIDReverse();

            //Backwards nature lock check
            gender = pid & 255;
            return (gender >= genderLower && gender <= genderUpper && pid % 25 == nature);
        }

        public bool salamenceShinySkip(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(1);

            //Check how many advances from shiny skip and build PID
            psv = getPSVReverse();
            psvtemp = getPSVReverse();
            while (psv == psvtemp)
            {
                psvtemp = psv;
                psv = getPSVReverse();
            }

            reverse.GetNext32BitNumber(5);
            pid = getPIDReverse();

            //Backwards nature lock check
            gender = pid & 255;
            return (gender >= genderLower && gender <= genderUpper && pid % 25 == nature);
        }

        public bool method1FirstShadow(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber();

            //Build temp pid first to not waste time populating if first backwards nl fails
            pidOriginal = getPIDReverse();

            //Backwards nature lock check
            gender = pidOriginal & 255;
            if (!(gender >= lockInfo[0].genderLower && gender <= lockInfo[0].genderUpper && pidOriginal % 25 == lockInfo[0].nature))
                return false;

            //Backwards nature lock check loop
            for (x = 1; x < backCount; x++)
            {
                reverse.GetNext32BitNumber(3);
                pid = getPIDReverse();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countBackTwo();
                }
            }

            forward.Seed = reverse.Seed;
            forward.GetNext32BitNumber();

            //Forwards nature lock check loop
            for (x = frontCount; x >= 0; x--)
            {
                forward.GetNext32BitNumber(3);
                pid = getPIDForward();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwo();
                }
            }

            return pidOriginal == pid;
        }

        public bool method1SecondShadowUnset(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(8);

            //Build temp pid first to not waste time populating if first nl fails
            pidOriginal = getPIDReverse();

            //Backwards nature lock check
            gender = pidOriginal & 255;
            if (!(gender >= lockInfo[0].genderLower && gender <= lockInfo[0].genderUpper && pidOriginal % 25 == lockInfo[0].nature))
                return false;

            //Backwards nature lock check loop
            for (x = 1; x < backCount; x++)
            {
                reverse.GetNext32BitNumber(3);
                pid = getPIDReverse();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countBackTwo();
                }
            }

            forward.Seed = reverse.Seed;
            forward.GetNext32BitNumber();

            //Forwards nature lock check loop
            for (x = frontCount; x >= 0; x--)
            {
                forward.GetNext32BitNumber(3);
                pid = getPIDForward();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwo();
                }
            }

            return pidOriginal == pid;
        }

        public bool method1SecondShadowSet(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(6);

            //Build temp pid first to not waste time populating if first nl fails
            pidOriginal = getPIDReverse();

            //Backwards nature lock check
            gender = pidOriginal & 255;
            if (!(gender >= lockInfo[0].genderLower && gender <= lockInfo[0].genderUpper && pidOriginal % 25 == lockInfo[0].nature))
                return false;

            //Backwards nature lock check loop
            for (x = 1; x < backCount; x++)
            {
                reverse.GetNext32BitNumber(3);
                pid = getPIDReverse();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countBackTwo();
                }
            }

            forward.Seed = reverse.Seed;
            forward.GetNext32BitNumber();

            //Forwards nature lock check loop
            for (x = frontCount; x >= 0; x--)
            {
                forward.GetNext32BitNumber(3);
                pid = getPIDForward();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwo();
                }
            }

            return pidOriginal == pid;
        }

        public bool method1SecondShadowShinySkip(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(1);

            //Check how many advances from shiny skip and build initial pid for first nl
            psv = getPSVReverse();
            psvtemp = getPSVReverse();
            while (psv == psvtemp)
            {
                psvtemp = psv;
                psv = getPSVReverse();
            }

            reverse.GetNext32BitNumber(5);
            pidOriginal = getPIDReverse();

            //Backwards nature lock check
            gender = pidOriginal & 255;
            if (!(gender >= lockInfo[0].genderLower && gender <= lockInfo[0].genderUpper && pidOriginal % 25 == lockInfo[0].nature))
                return false;

            //Backwards nature lock check loop
            for (x = 1; x < backCount; x++)
            {
                reverse.GetNext32BitNumber(3);
                pid = getPIDReverse();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countBackTwo();
                }
            }

            forward.Seed = reverse.Seed;
            forward.GetNext32BitNumber();

            //Forwards nature lock check loop
            for (x = frontCount; x >= 0; x--)
            {
                forward.GetNext32BitNumber(3);
                pid = getPIDForward();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwo();
                }
            }

            return pidOriginal == pid;
        }

        public void method2FirstShadow(bool xor, out uint seed, out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;
            sister = xor;

            for (x = shadowCount; x >= 0; x--)
            {
                forwardCounter += 4;
                pid = getPIDForward2();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwo2();
                }
            }

            forwardCounter += 2;
            seed = sister ? rand[forwardCounter++] ^ 0x80000000 : rand[forwardCounter++];
            iv1 = rand[forwardCounter++] >> 16;
            iv2 = rand[forwardCounter] >> 16;
            forwardCounter += 2;
            pid = getPIDForward2();
        }

        public int method2SecondShadowPassNL(bool xor)
        {
            forwardCounter = 5;
            sister = xor;

            for (x = shadowCount; x >= 0; x--)
            {
                forwardCounter += 4;
                pid = getPIDForward2();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwo2();
                }
            }

            return forwardCounter;
        }

        public void method2SecondShadowSet(bool xor, int count, out uint seed, out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = count + 7;
            sister = xor;
            seed = sister ? rand[forwardCounter++] ^ 0x80000000 : rand[forwardCounter++];
            iv1 = rand[forwardCounter++] >> 16;
            iv2 = rand[forwardCounter] >> 16;
            forwardCounter += 2; 
            pid = getPIDForward2();
        }

        public void method2SecondShadowUnset(bool xor, int count, out uint seed, out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = count + 9;
            sister = xor;
            seed = sister ? rand[forwardCounter++] ^ 0x80000000 : rand[forwardCounter++];
            iv1 = rand[forwardCounter++] >> 16;
            iv2 = rand[forwardCounter] >> 16;
            forwardCounter += 2;
            pid = getPIDForward2();
        }

        public void method2SecondShinySkip(bool xor, int count, out uint seed, out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = count + 6;
            sister = xor;
            psv = getPSVForward2();
            forwardCounter++;
            psvtemp = getPSVForward2();
            while (psv == psvtemp)
            {
                psvtemp = psv;
                forwardCounter++;
                psv = getPSVForward2();
            }

            forwardCounter += 2;
            seed = sister ? rand[forwardCounter++] ^ 0x80000000 : rand[forwardCounter++];
            iv1 = rand[forwardCounter++] >> 16;
            iv2 = rand[forwardCounter] >> 16;
            forwardCounter += 2;
            pid = getPIDForward2();
        }

        public void methodShadowFirstShadow(out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;

            for (x = shadowCount; x >= 0; x--)
            {
                forwardCounter += 4;
                pid = getPIDShadow();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwoShadow();
                }
            }

            forwardCounter += 3;
            iv1 = rand[forwardCounter++] >> 16;
            iv2 = rand[forwardCounter] >> 16;
            forwardCounter += 2;
            pid = getPIDShadow();
        }

        public void methodShadowSecondShadowSet(out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;

            for (x = shadowCount; x >= 0; x--)
            {
                forwardCounter += 4;
                pid = getPIDShadow();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwoShadow();
                }
            }

            forwardCounter += 8;
            iv1 = rand[forwardCounter++] >> 16;
            iv2 = rand[forwardCounter] >> 16;
            forwardCounter += 2;
            pid = getPIDShadow();
        }

        public void methodShadowSecondShadowUnset(out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;

            for (x = shadowCount; x >= 0; x--)
            {
                forwardCounter += 4;
                pid = getPIDShadow();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwoShadow();
                }
            }

            forwardCounter += 10;
            iv1 = rand[forwardCounter++] >> 16;
            iv2 = rand[forwardCounter] >> 16;
            forwardCounter += 2;
            pid = getPIDShadow();
        }

        public void methodShadowSecondShinySkip(out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;

            for (x = shadowCount; x >= 0; x--)
            {
                forwardCounter += 4;
                pid = getPIDShadow();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwoShadow();
                }
            }

            forwardCounter += 6;
            psv = getPSVShadow();
            forwardCounter++;
            psvtemp = getPSVShadow();
            while (psv == psvtemp)
            {
                psvtemp = psv;
                forwardCounter++;
                psv = getPSVShadow();
            }

            forwardCounter += 3;
            iv1 = rand[forwardCounter++] >> 16;
            iv2 = rand[forwardCounter] >> 16;
            forwardCounter += 2;
            pid = getPIDShadow();
        }

        public ShadowType getType()
        {
            return type;
        }

        private uint getPIDReverse()
        {
            return reverse.GetNext16BitNumber() | (reverse.GetNext32BitNumber() & 0xFFFF0000);
        }

        private uint getPSVReverse()
        {
            return (reverse.GetNext16BitNumber() ^ reverse.GetNext16BitNumber()) >> 3;
        }

        private uint getPIDForward()
        {
            return (forward.GetNext32BitNumber() & 0xFFFF0000) | forward.GetNext16BitNumber();
        }

        private uint getPIDShadow()
        {
            return (rand[forwardCounter++] & 0xFFFF0000) | (rand[forwardCounter] >> 16);
        }

        private uint getPSVShadow()
        {
            return ((rand[forwardCounter++] >> 16) ^ (rand[forwardCounter] >> 16)) >> 3;
        }

        private uint getPIDForward2()
        {
            return sister ? ((rand[forwardCounter++] & 0xFFFF0000) | (rand[forwardCounter] >> 16)) ^ 0x80008000 : (rand[forwardCounter++] & 0xFFFF0000) | (rand[forwardCounter] >> 16);
        }

        private uint getPSVForward2()
        {
            return ((rand[forwardCounter++] >> 16) ^ (rand[forwardCounter] >> 16)) >> 3;
        }

        private void countBackTwo()
        {
            do
            {
                pid = getPIDReverse();
                gender = pid & 255;
            } while (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature));
        }

        private void countForwardTwo()
        {
            do
            {
                pid = getPIDForward();
                gender = pid & 255;
            } while (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature));
        }

        private void countForwardTwo2()
        {
            do
            {
                forwardCounter++;
                pid = getPIDForward2();
                gender = pid & 255;
            } while (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature));
        }

        private void countForwardTwoShadow()
        {
            do
            {
                forwardCounter++;
                pid = getPIDShadow();
                gender = pid & 255;
            } while (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature));
        }

        private void getCurrLock()
        {
            nature = lockInfo[x].nature;
            genderLower = lockInfo[x].genderLower;
            genderUpper = lockInfo[x].genderUpper;
        }

        public enum ShadowType
        {
            NoLock,
            SingleLock,
            FirstShadow,
            Salamence,
            SecondShadow
        }
    }

    class LockInfo
    {
        public uint nature { get; set; }

        public uint genderLower { get; set; }

        public uint genderUpper { get; set; }

        public LockInfo(uint nature, uint genderLower, uint genderUpper)
        {
            this.nature = nature;
            this.genderLower = genderLower;
            this.genderUpper = genderUpper;
        }
    }
}