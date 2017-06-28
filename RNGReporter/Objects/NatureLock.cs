using System.Collections.Generic;

namespace RNGReporter.Objects
{
    public class NatureLock
    {
        private LockInfo[] lockInfo;
        private int forwardCounter, count, count2, x;
        private uint pid, pidOriginal, gender, genderLower, genderUpper, nature;
        public List<uint> rand;
        private XdRngR reverse;
        private XdRng forward;

        public NatureLock(int lockNum)
        {
            lockInfo = natureLockList(lockNum);
            rand = new List<uint>();
            count = lockInfo.Length;
            count2 = count == 1 ? 0 : count - 2;
            reverse = new XdRngR(0);
            forward = new XdRng(0);
            x = 0;
            if (count == 1)
                getCurrLock();
        }

        private LockInfo[] natureLockList(int natureLockIndex)
        {
            switch (natureLockIndex)
            {
                case 0:
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(0, 0, 126), new LockInfo(12, 127, 255) }; //Altaria
                case 1:
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(12, 0, 126), new LockInfo(0, 0, 126), new LockInfo(6, 127, 255) }; //Arbok
                case 2:
                    return null; //Articuno 
                case 3:
                    return null; //Baltoy 3
                case 4:
                    return null; //Baltoy 1
                case 5:
                    return new LockInfo[] { new LockInfo(0, 127, 255), new LockInfo(24, 127, 255) }; //Baltoy 2
                case 6:
                    return new LockInfo[] { new LockInfo(12, 0, 255), new LockInfo(18, 0, 126), new LockInfo(0, 0, 255) }; //Banette
                case 7:
                    return null; //Beedrill
                case 8:
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(6, 127, 255), new LockInfo(12, 0, 190) }; //Butterfree
                case 9:
                    return null; //Carvanha
                case 10:
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(6, 0, 126) }; //Chansey
                case 11:
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(0, 127, 255), new LockInfo(6, 0, 190) }; //Delcatty
                case 12:
                    return new LockInfo[] { new LockInfo(18, 0, 126) }; //Dodrio
                case 13:
                    return new LockInfo[] { new LockInfo(0, 127, 255), new LockInfo(12, 0, 126), new LockInfo(12, 0, 126), new LockInfo(18, 127, 255), new LockInfo(0, 127, 255) }; //Dragonite
                case 14:
                    return new LockInfo[] { new LockInfo(12, 127, 255), new LockInfo(6, 0, 126), new LockInfo(18, 127, 255), new LockInfo(0, 127, 255) }; //Dugtrio
                case 15:
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(18, 0, 126), new LockInfo(12, 127, 255) }; //Duskull
                case 16:
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 0, 126), new LockInfo(24, 63, 255) }; //Electabuzz
                case 17:
                    return null; //Exeggutor
                case 18:
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(0, 0, 126), new LockInfo(12, 127, 255) }; //Farfetch'd  
                case 19:
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 0, 126), new LockInfo(24, 127, 255) }; //Golduck
                case 20:
                    return new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(12, 127, 255) }; //Grimer
                case 21:
                    return new LockInfo[] { new LockInfo(6, 0, 126), new LockInfo(24, 127, 255) }; //Growlithe
                case 22:
                    return new LockInfo[] { new LockInfo(6, 127, 255), new LockInfo(12, 0, 126) }; //Gulpin 3
                case 23:
                    return new LockInfo[] { new LockInfo(6, 127, 255), new LockInfo(12, 0, 126) }; //Gulpin 1
                case 24:
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(0, 0, 126), new LockInfo(6, 127, 255), new LockInfo(12, 0, 126) }; //Gulpin 2
                case 25:
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 0, 126), new LockInfo(24, 127, 255) }; //Hitmonchan
                case 26:
                    return new LockInfo[] { new LockInfo(24, 0, 126), new LockInfo(6, 0, 255), new LockInfo(12, 0, 126), new LockInfo(18, 127, 255) }; //Hitmonlee
                case 27:
                    return null; //Houndour 3
                case 28:
                    return null; //Houndour 1
                case 29:
                    return null; //To do houndour 2
                case 30:
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(6, 0, 126), new LockInfo(12, 0, 126), new LockInfo(18, 0, 126) }; //Hypno
                case 31:
                    return new LockInfo[] { new LockInfo(12, 0, 255), new LockInfo(18, 0, 126), new LockInfo(0, 0, 255) }; //Kangaskhan
                case 32:
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(500, 500, 500), new LockInfo(500, 500, 500), new LockInfo(6, 0, 126) }; //Lapras
                case 33:
                    return new LockInfo[] { new LockInfo(0, 0, 126) }; //Ledyba
                case 34:
                    return new LockInfo[] { new LockInfo(6, 0, 255), new LockInfo(24, 127, 255) }; //Lickitung
                case 35:
                    return null; //Lugia
                case 36:
                    return new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(0, 0, 126) }; //Lunatone
                case 37:
                    return new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(6, 127, 255), new LockInfo(24, 127, 255) }; //Marcargo
                case 38:
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(18, 191, 255), new LockInfo(18, 127, 255) }; //Magmar 
                case 39:
                    return new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(0, 127, 255), new LockInfo(18, 0, 255) }; //Magneton
                case 40:
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 127, 255) }; //Makuhita
                case 41:
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(24, 127, 255) }; //Makuhita Colo
                case 42:
                    return new LockInfo[] { new LockInfo(6, 0, 126) }; //Manectric
                case 43:
                    return null; //Mareep 3
                case 44:
                    return new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(24, 127, 255) }; //Mareep 1
                case 45:
                    return new LockInfo[] { new LockInfo(0, 0, 255), new LockInfo(12, 0, 126), new LockInfo(24, 127, 255) }; //Mareep 2
                case 46:
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(500, 500, 500), new LockInfo(500, 500, 500), new LockInfo(6, 0, 126) }; //Marowak
                case 47:
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 127, 255) }; //Mawile
                case 48:
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(0, 0, 126), new LockInfo(6, 63, 255) }; //Meowth
                case 49:
                    return null; //Moltres
                case 50:
                    return new LockInfo[] { new LockInfo(6, 0, 126), new LockInfo(24, 127, 255), new LockInfo(18, 127, 255), new LockInfo(18, 127, 255) }; //Mr. Mime
                case 51:
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(24, 127, 255) }; //Natu
                case 52:
                    return new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(18, 127, 255), new LockInfo(0, 127, 255) }; //Nosepass
                case 53:
                    return new LockInfo[] { new LockInfo(24, 0, 126), new LockInfo(0, 0, 255), new LockInfo(6, 127, 255) }; //Numel
                case 54:
                    return new LockInfo[] { new LockInfo(6, 0, 126), new LockInfo(24, 127, 255) }; //Paras
                case 55:
                    return new LockInfo[] { new LockInfo(18, 32, 255), new LockInfo(12, 127, 255) }; //Pidgeotto
                case 56:
                    return new LockInfo[] { new LockInfo(6, 127, 255) }; //Pineco
                case 57:
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(18, 191, 255), new LockInfo(18, 127, 255) }; //Pinsir
                case 58:
                    return new LockInfo[] { new LockInfo(6, 0, 126), new LockInfo(24, 127, 255), new LockInfo(18, 127, 255), new LockInfo(18, 127, 255) }; //Poliwrath
                case 59:
                    return new LockInfo[] { new LockInfo(12, 0, 126) }; //Poochyena
                case 60:
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(6, 0, 126), new LockInfo(12, 0, 126), new LockInfo(18, 0, 126) }; //Primeape
                case 61:
                    return new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(6, 0, 126), new LockInfo(0, 63, 255) }; //Ralts
                case 62:
                    return new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(6, 127, 255), new LockInfo(24, 127, 255) }; //Rapidash
                case 63:
                    return new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(500, 500, 500), new LockInfo(18, 0, 126) }; //Raticate
                case 64:
                    return null; //Rhydon
                case 65:
                    return new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(6, 127, 255) }; //Roselia
                case 66:
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 0, 126), new LockInfo(24, 127, 255) }; //Sableye
                case 67:
                    return new LockInfo[] { new LockInfo(6, 0, 126) }; //Salamence
                case 68:
                    return new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(6, 0, 126) }; //Scyther
                case 69:
                    return null; //To do seedot 3
                case 70:
                    return new LockInfo[] { new LockInfo(12, 127, 255), new LockInfo(0, 127, 255), new LockInfo(12, 0, 126), new LockInfo(24, 0, 126), new LockInfo(6, 127, 255) }; //Seedot 1
                case 71:
                    return new LockInfo[] { new LockInfo(6, 127, 255), new LockInfo(0, 0, 126), new LockInfo(0, 0, 126), new LockInfo(24, 0, 126), new LockInfo(6, 127, 255) }; //Seedot 2
                case 72:
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(12, 127, 255), new LockInfo(6, 127, 255) }; //Seel
                case 73:
                    return null; //Shellder
                case 74:
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(24, 0, 126) }; //Shroomish
                case 75:
                    return new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 0, 126), new LockInfo(24, 63, 255) }; //Snorlax
                case 76:
                    return new LockInfo[] { new LockInfo(6, 0, 126) }; //Snorunt
                case 77:
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(6, 127, 255), new LockInfo(24, 0, 255) }; //Solrock
                case 78:
                    return new LockInfo[] { new LockInfo(6, 0, 126), new LockInfo(18, 127, 255) }; //Spearow
                case 79:
                    return new LockInfo[] { new LockInfo(0, 0, 255), new LockInfo(12, 0, 126), new LockInfo(24, 127, 255) }; //Spheal 3
                case 80:
                    return new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(24, 127, 255) }; //Spheal 1
                case 81:
                    return new LockInfo[] { new LockInfo(0, 0, 255), new LockInfo(12, 0, 126), new LockInfo(24, 127, 255) }; //Spheal 2
                case 82:
                    return new LockInfo[] { new LockInfo(6, 127, 255), new LockInfo(12, 0, 126) }; //Spinarak
                case 83:
                    return new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(500, 500, 500), new LockInfo(0, 0, 126), new LockInfo(6, 127, 255), new LockInfo(24, 0, 255) }; //Starmie
                case 84:
                    return null; //Swellow
                case 85:
                    return new LockInfo[] { new LockInfo(0, 127, 255), new LockInfo(18, 0, 126) }; //Swinub
                case 86:
                    return new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(6, 127, 255), new LockInfo(12, 0, 190) }; //Tangela
                case 87:
                    return null; //Tauros
                case 88:
                    return null; //Teddiursa
                case 89:
                    return null; //Togepi
                case 90:
                    return new LockInfo[] { new LockInfo(12, 127, 255), new LockInfo(24, 0, 255), new LockInfo(18, 0, 126) }; //Venomoth
                case 91:
                    return new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(12, 127, 255), new LockInfo(0, 127, 255) }; //Voltorb
                case 92:
                    return new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(6, 0, 126), new LockInfo(0, 127, 255) }; //Vulpix
                case 93:
                    return new LockInfo[] { new LockInfo(12, 127, 255), new LockInfo(24, 0, 255), new LockInfo(18, 0, 126) }; //Weepinbell
                case 94:
                    return null; //Zangoose
                default:
                    return null; //Zapdos 
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
            return !(gender < genderLower || gender > genderUpper || pid % 25 != nature);
        }

        public bool salamenceUnset(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(13);

            //Build PID
            pid = getPIDReverse();

            //Backwards nature lock check
            gender = pid & 255;
            return !(gender < genderLower || gender > genderUpper || pid % 25 != nature);
        }

        public bool salamenceSet(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(11);

            //Build PID
            pid = getPIDReverse();

            //Backwards nature lock check
            gender = pid & 255;
            return !(gender < genderLower || gender > genderUpper || pid % 25 != nature);
        }

        public bool salamenceShinySkip(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(1);

            uint psv, psvtemp;

            //Check how many advances from shiny skip and build PID
            psv = getPSVReverse();
            psvtemp = getPSVReverse();
            while (psv == psvtemp)
            {
                psvtemp = psv;
                psv = getPSVReverse();
            }

            reverse.GetNext32BitNumber(10);
            pid = getPIDReverse();

            //Backwards nature lock check
            gender = pid & 255;
            return !(gender < genderLower || gender > genderUpper || pid % 25 != nature);
        }

        public bool method1FirstShadow(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber();

            //Build temp pid first to not waste time populating if first backwards nl fails
            pidOriginal = getPIDReverse();

            //Backwards nature lock check
            gender = pidOriginal & 255;
            if (gender < lockInfo[0].genderLower || gender > lockInfo[0].genderUpper || pidOriginal % 25 != lockInfo[0].nature)
                return false;

            //Backwards nature lock check loop
            for (x = 1; x < count; x++)
            {
                reverse.GetNext32BitNumber(3);
                pid = getPIDReverse();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (gender < genderLower || gender > genderUpper || pid % 25 != nature)
                        countBackTwo();
                }
            }

            forward.Seed = reverse.Seed;
            forward.GetNext32BitNumber();

            //Forwards nature lock check loop
            for (x = count2; x >= 0; x--)
            {
                forward.GetNext32BitNumber(3);
                pid = getPIDForward();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (gender < genderLower || gender > genderUpper || pid % 25 != nature)
                        countForwardTwo();
                }
            }

            return pidOriginal == pid;
        }

        public bool method1SecondShadowUnset(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(13);

            //Build temp pid first to not waste time populating if first nl fails
            pidOriginal = getPIDReverse();

            //Backwards nature lock check
            gender = pidOriginal & 255;
            if (gender < lockInfo[0].genderLower || gender > lockInfo[0].genderUpper || pidOriginal % 25 != lockInfo[0].nature)
                return false;

            //Backwards nature lock check loop
            for (x = 1; x < count; x++)
            {
                reverse.GetNext32BitNumber(3);
                pid = getPIDReverse();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (gender < genderLower || gender > genderUpper || pid % 25 != nature)
                        countBackTwo();
                }
            }

            forward.Seed = reverse.Seed;
            forward.GetNext32BitNumber();

            //Forwards nature lock check loop
            for (x = count2; x >= 0; x--)
            {
                forward.GetNext32BitNumber(3);
                pid = getPIDForward();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (gender < genderLower || gender > genderUpper || pid % 25 != nature)
                        countForwardTwo();
                }
            }

            return pidOriginal == pid;
        }

        public bool method1SecondShadowSet(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(11);

            //Build temp pid first to not waste time populating if first nl fails
            pidOriginal = getPIDReverse();

            //Backwards nature lock check
            gender = pidOriginal & 255;
            if (gender < lockInfo[0].genderLower || gender > lockInfo[0].genderUpper || pidOriginal % 25 != lockInfo[0].nature)
                return false;

            //Backwards nature lock check loop
            for (x = 1; x < count; x++)
            {
                reverse.GetNext32BitNumber(3);
                pid = getPIDReverse();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (gender < genderLower || gender > genderUpper || pid % 25 != nature)
                        countBackTwo();
                }
            }

            forward.Seed = reverse.Seed;
            forward.GetNext32BitNumber();

            //Forwards nature lock check loop
            for (x = count2; x >= 0; x--)
            {
                forward.GetNext32BitNumber(3);
                pid = getPIDForward();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (gender < genderLower || gender > genderUpper || pid % 25 != nature)
                        countForwardTwo();
                }
            }

            return pidOriginal == pid;
        }

        public bool method1SecondShadowShinySkip(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(1);

            uint pidOriginal, psv, psvtemp;

            //Check how many advances from shiny skip and build initial pid for first nl
            psv = getPSVReverse();
            psvtemp = getPSVReverse();
            while (psv == psvtemp)
            {
                psvtemp = psv;
                psv = getPSVReverse();
            }

            reverse.GetNext32BitNumber(10);
            pidOriginal = getPIDReverse();

            //Backwards nature lock check
            gender = pidOriginal & 255;
            if (gender < lockInfo[0].genderLower || gender > lockInfo[0].genderUpper || pidOriginal % 25 != lockInfo[0].nature)
                return false;

            //Backwards nature lock check loop
            for (x = 1; x < count; x++)
            {
                reverse.GetNext32BitNumber(3);
                pid = getPIDReverse();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (gender < genderLower || gender > genderUpper || pid % 25 != nature)
                        countBackTwo();
                }
            }

            forward.Seed = reverse.Seed;
            forward.GetNext32BitNumber();

            //Forwards nature lock check loop
            for (x = count2; x >= 0; x--)
            {
                forward.GetNext32BitNumber(3);
                pid = getPIDForward();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (gender < genderLower || gender > genderUpper || pid % 25 != nature)
                        countForwardTwo();
                }
            }

            return pidOriginal == pid;
        }

        public void method2FirstShadow(bool sister, out uint seed, out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;

            for (x = count2; x >= 0; x++)
            {
                forwardCounter += 5;
                pid = getPIDForward2(sister);
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (gender < genderLower || gender > genderUpper || pid % 25 != nature)
                        countForwardTwo2(sister);
                }
            }

            forwardCounter += 2;
            seed = sister ? rand[forwardCounter] ^ 0x80000000 : rand[forwardCounter];
            forwardCounter++;
            iv1 = sister ? (rand[forwardCounter] >> 16) ^ 0x8000 : rand[forwardCounter] >> 16;
            forwardCounter++;
            iv2 = sister ? (rand[forwardCounter] >> 16) ^ 0x8000 : rand[forwardCounter] >> 16;
            forwardCounter += 3;
            pid = getPIDForward2(sister);
        }

        public void method2SecondShadowSet(bool sister, out uint seed, out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;

            for (x = count2; x >= 0; x++)
            {
                forwardCounter += 5;
                pid = getPIDForward2(sister);
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (gender < genderLower || gender > genderUpper || pid % 25 != nature)
                        countForwardTwo2(sister);
                }
            }

            forwardCounter += 7;
            seed = sister ? rand[forwardCounter] ^ 0x80000000 : rand[forwardCounter];
            forwardCounter++;
            iv1 = sister ? (rand[forwardCounter] >> 16) ^ 0x8000 : rand[forwardCounter] >> 16;
            forwardCounter++;
            iv2 = sister ? (rand[forwardCounter] >> 16) ^ 0x8000 : rand[forwardCounter] >> 16;
            forwardCounter += 3;
            pid = getPIDForward2(sister);
        }

        public void method2SecondShadowUnset(bool sister, out uint seed, out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;

            for (x = count2; x >= 0; x++)
            {
                forwardCounter += 5;
                pid = getPIDForward2(sister);
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (gender < genderLower || gender > genderUpper || pid % 25 != nature)
                        countForwardTwo2(sister);
                }
            }

            forwardCounter += 9;
            seed = sister ? rand[forwardCounter] ^ 0x80000000 : rand[forwardCounter];
            forwardCounter++;
            iv1 = sister ? (rand[forwardCounter] >> 16) ^ 0x8000 : rand[forwardCounter] >> 16;
            forwardCounter++;
            iv2 = sister ? (rand[forwardCounter] >> 16) ^ 0x8000 : rand[forwardCounter] >> 16;
            forwardCounter += 3;
            pid = getPIDForward2(sister);
        }

        public void method2SecondShinySkip(bool sister, out uint seed, out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 4;

            for (x = count2; x >= 0; x++)
            {
                forwardCounter += 5;
                pid = getPIDForward2(sister);
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (gender < genderLower || gender > genderUpper || pid % 25 != nature)
                        countForwardTwo2(sister);
                }
            }

            forwardCounter += 7;
            uint psv, psvtemp;
            psv = getPSVForward2();
            forwardCounter += 2;
            psvtemp = getPSVForward2();
            while (psv == psvtemp)
            {
                psvtemp = psv;
                forwardCounter += 2;
                psv = getPSVForward2();
            }

            forwardCounter += 2;
            seed = sister ? rand[forwardCounter] ^ 0x80000000 : rand[forwardCounter];
            forwardCounter++;
            iv1 = sister ? (rand[forwardCounter - 4] >> 16) ^ 0x8000 : rand[forwardCounter - 4] >> 16;
            forwardCounter++;
            iv2 = sister ? (rand[forwardCounter - 3] >> 16) ^ 0x8000 : rand[forwardCounter - 3] >> 16;
            forwardCounter += 3;
            pid = getPIDForward2(sister);
        }

        public void methodShadowFirstShadow(out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;

            for (x = count2; x >= 0; x++)
            {
                forwardCounter += 5;
                pid = getPIDShadow();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (gender < genderLower || gender > genderUpper || pid % 25 != nature)
                        countForwardTwoShadow();
                }
            }

            forwardCounter += 7;
            pid = getPIDShadow();
            iv1 = rand[forwardCounter - 4] >> 16;
            iv2 = rand[forwardCounter - 3] >> 16;
        }

        public void methodShadowSecondShadowSet(out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;

            for (x = count2; x >= 0; x++)
            {
                forwardCounter += 5;
                pid = getPIDShadow();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (gender < genderLower || gender > genderUpper || pid % 25 != nature)
                        countForwardTwoShadow();
                }
            }

            forwardCounter += 12;
            pid = getPIDShadow();
            iv1 = rand[forwardCounter - 4] >> 16;
            iv2 = rand[forwardCounter - 3] >> 16;
        }

        public void methodShadowSecondShadowUnset(out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;

            for (x = count2; x >= 0; x++)
            {
                forwardCounter += 5;
                pid = getPIDShadow();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (gender < genderLower || gender > genderUpper || pid % 25 != nature)
                        countForwardTwoShadow();
                }
            }

            forwardCounter += 14;
            pid = getPIDShadow();
            iv1 = rand[forwardCounter - 4] >> 16;
            iv2 = rand[forwardCounter - 3] >> 16;
        }

        public void methodShadowSecondShinySkip(out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;

            for (x = count2; x >= 0; x++)
            {
                forwardCounter += 5;
                pid = getPIDShadow();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (gender < genderLower || gender > genderUpper || pid % 25 != nature)
                        countForwardTwoShadow();
                }
            }

            forwardCounter += 7;
            uint psv, psvtemp;
            psv = getPSVShadow();
            forwardCounter += 2;
            psvtemp = getPSVShadow();
            while (psv == psvtemp)
            {
                psvtemp = psv;
                forwardCounter += 2;
                psv = getPSVShadow();
            }

            forwardCounter += 7;
            pid = getPIDShadow();
            iv1 = rand[forwardCounter - 4] >> 16;
            iv2 = rand[forwardCounter - 3] >> 16;
        }

        public uint getType(int natureLockIndex)
        {
            switch (natureLockIndex)
            {
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
                    return 0;
                case 1: //Arbok
                case 5: //Baltoy 2
                case 11: //Delcatty
                case 13: //Dragonite
                case 14: //Dugtrio
                case 15: //Duskull
                case 16: //Electabuzz
                case 18: //Farfetch'd  
                case 19: //Golduck
                case 20: //Grimer
                case 22: //Gulpin 3
                case 23: //Gulpin 1
                case 24: //Gulpin 2
                case 25: //Hitmonchan
                case 26: //Hitmonlee
                case 31: //Kangaskhan
                case 34: //Lickitung
                case 36: //Lunatone
                case 38: //Magmar 
                case 39: //Magneton
                case 40: //Makuhita
                case 41: //Makuhita Colo
                case 44: //Mareep 1
                case 45: //Mareep 2
                case 46: //Marowak
                case 47: //Mawile
                case 48: //Meowth
                case 51: //Natu
                case 52: //Nosepass
                case 53: //Numel
                case 54: //Paras
                case 55: //Pidgeotto
                case 58: //Poliwrath
                case 60: //Primeape
                case 61: //Ralts
                case 62: //Rapidash
                case 63: //Raticate
                case 65: //Roselia
                case 68: //Scyther
                case 70: //Seedot 1
                case 71: //Seedot 2
                case 72: //Seel
                case 74: //Shroomish
                case 77: //Solrock
                case 78: //Spearow
                case 79: //Spheal 3
                case 80: //Spheal 1
                case 81: //Spheal 2
                case 82: //Spinarak
                case 83: //Starmie
                case 85: //Swinub
                case 86: //Tangela
                case 90: //Venomoth
                case 91: //Voltorb
                case 92: //Vulpix
                case 12: //Dodrio
                case 33: //Ledyba
                case 42: //Manectric
                case 56: //Pineco
                case 59: //Poochyena
                case 76: //Snorunt
                    return 2;
                case 67: //Salamence
                    return 3;
                case 0: //Altaria
                case 6: //Banette
                case 8: //Butterfree
                case 10: //Chansey
                case 21: //Growlithe
                case 30: //Hypno
                case 32: //Lapras
                case 37: //Marcargo
                case 50: //Mr. Mime
                case 57: //Pinsir
                case 66: //Sableye
                case 75: //Snorlax
                case 93: //Weepinbell
                    return 6;
            }
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
            return (rand[forwardCounter - 1] & 0xFFFF0000) | (rand[forwardCounter] >> 16);
        }

        private uint getPSVShadow()
        {
            return (rand[forwardCounter - 1] ^ rand[forwardCounter]) >> 3;
        }

        private uint getPIDForward2(bool sister)
        {
            return sister ? ((rand[forwardCounter - 1] & 0xFFFF0000) | (rand[forwardCounter] >> 16)) ^ 0x80008000 : (rand[forwardCounter - 1] & 0xFFFF0000) | (rand[forwardCounter] >> 16);
        }

        private uint getPSVForward2()
        {
            return (rand[forwardCounter - 1] ^ rand[forwardCounter]) >> 3;
        }

        private void countBackTwo()
        {
            pid = getPIDReverse();
            gender = pid & 255;
            while (gender < genderLower || gender > genderUpper || pid % 25 != nature)
            {
                pid = getPIDReverse();
                gender = pid & 255;
            }
        }

        private void countForwardTwo()
        {
            pid = getPIDForward();
            gender = pid & 255;
            while (gender < genderLower || gender > genderUpper || pid % 25 != nature)
            {
                pid = getPIDForward();
                gender = pid & 255;
            }
        }

        private void countForwardTwo2(bool sister)
        {
            forwardCounter += 2;
            pid = getPIDForward2(sister);
            gender = pid & 255;
            while (gender < genderLower || gender > genderUpper || pid % 25 != nature)
            {
                forwardCounter += 2;
                pid = getPIDForward2(sister);
                gender = pid & 255;
            }
        }

        private void countForwardTwoShadow()
        {
            forwardCounter += 2;
            pid = getPIDShadow();
            gender = pid & 255;
            while (gender < genderLower || gender > genderUpper || pid % 25 != nature)
            {
                forwardCounter += 2;
                pid = getPIDShadow();
                gender = pid & 255;
            }
        }

        private void getCurrLock()
        {
            nature = lockInfo[x].nature;
            genderLower = lockInfo[x].genderLower;
            genderUpper = lockInfo[x].genderUpper;
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