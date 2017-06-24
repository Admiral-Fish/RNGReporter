using System.Collections.Generic;

namespace RNGReporter.Objects
{
    public class NatureLock
    {
        private LockInfo[] lockInfo;
        private static int forwardCounter, count, count2;
        private static uint pid, genderval;
        public List<uint> rand;
        public int index;
        private XdRngR reverse;
        private XdRng forward;

        public NatureLock(int lockNum)
        {
            lockInfo = natureLockList(lockNum);
            rand = new List<uint>();
            count = lockInfo.Length;
            count2 = lockNum == 12 || lockNum == 12 || lockNum == 33 || lockNum == 42 || lockNum == 56 || lockNum == 59 || lockNum == 76 ||lockNum == 67 ? 1 : count - 2;
            reverse = new XdRngR(0);
            forward = new XdRng(0);
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
            uint genderval = pid & 255;
            if (genderval < lockInfo[0].genderLower || genderval > lockInfo[0].genderUpper || pid % 25 != lockInfo[0].nature)
                return false;
            else
                return true;
        }

        public bool salamenceUnset(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(8);

            //Build PID
            pid = getPIDReverse();

            //Backwards nature lock check
            genderval = pid & 255;
            if (genderval < lockInfo[0].genderLower || genderval > lockInfo[0].genderUpper || pid % 25 != lockInfo[0].nature)
                return false;
            else
                return true;
        }

        public bool salamenceSet(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(6);

            //Build PID
            pid = getPIDReverse();

            //Backwards nature lock check
            genderval = pid & 255;
            if (genderval < lockInfo[0].genderLower || genderval > lockInfo[0].genderUpper || pid % 25 != lockInfo[0].nature)
                return false;
            else
                return true;
        }

        public bool salamenceShinySkip(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(1);

            uint psv, psvtemp;
            bool shinyFlag = true;

            //Check how many advances from shiny skip and build PID
            pid = getPIDReverse();
            psv = ((pid & 0xFFFF) ^ (pid >> 16)) >> 3;
            while (shinyFlag)
            {
                pid = getPIDReverse();
                psvtemp = ((pid & 0xFFFF) ^ (pid >> 16)) >> 3;
                if (psvtemp != psv)
                    shinyFlag = false;
                else
                    psv = psvtemp;
            }

            reverse.GetNext32BitNumber(10);
            pid = getPIDReverse();

            //Backwards nature lock check
            genderval = pid & 255;
            if (genderval < lockInfo[0].genderLower || genderval > lockInfo[0].genderUpper || pid % 25 != lockInfo[0].nature)
                return false;
            else
                return true;
        }

        public bool method1FirstShadow(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber();

            //Build temp pid first to not waste time populating if first backwards nl fails
            uint pidOriginal = getPIDReverse();

            //Backwards nature lock check
            uint genderval = pidOriginal & 255;
            if (genderval < lockInfo[0].genderLower || genderval > lockInfo[0].genderUpper || pidOriginal % 25 != lockInfo[0].nature)
                return false;

            for (int x = 1; x < count; x++)
            {
                reverse.GetNext32BitNumber(3);
                pid = getPIDReverse();
                if (lockInfo[x].nature != 500)
                {
                    genderval = pid & 255;
                    if (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
                        countBackTwo(x);
                }
            }

            forward.Seed = reverse.Seed;
            forward.GetNext32BitNumber();

            //Forwards nature lock check
            for (int x = count2; x >= 0; x--)
            {
                forward.GetNext32BitNumber(3);
                pid = getPIDForward();
                if (lockInfo[x].nature != 500)
                {
                    genderval = pid & 255;
                    if (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
                        countForwardTwo(x);
                }
            }

            return pidOriginal == pid;
        }

        public bool method1SecondShadowUnset(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(8);

            //Build temp pid first to not waste time populating if first nl fails
            uint pidOriginal = getPIDReverse();

            //Backwards nature lock check
            genderval = pidOriginal & 255;
            if (genderval < lockInfo[0].genderLower || genderval > lockInfo[0].genderUpper || pidOriginal % 25 != lockInfo[0].nature)
                return false;

            for (int x = 1; x < count; x++)
            {
                reverse.GetNext32BitNumber(3);
                pid = getPIDReverse();
                if (lockInfo[x].nature != 500)
                {
                    genderval = pid & 255;
                    if (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
                        countBackTwo(x);
                }
            }

            forward.Seed = reverse.Seed;
            forward.GetNext32BitNumber();

            //Forwards nature lock check
            for (int x = count2; x <= 0; x--)
            {
                forward.GetNext32BitNumber(3);
                pid = getPIDForward();
                if (lockInfo[x].nature != 500)
                {
                    genderval = pid & 255;
                    if (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
                        countForwardTwo(x);
                }
            }

            return pidOriginal == pid;
        }

        public bool method1SecondShadowSet(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(6);

            //Build temp pid first to not waste time populating if first nl fails
            uint pidOriginal = getPIDReverse();

            //Backwards nature lock check
            genderval = pidOriginal & 255;
            if (genderval < lockInfo[0].genderLower || genderval > lockInfo[0].genderUpper || pidOriginal % 25 != lockInfo[0].nature)
                return false;

            for (int x = 1; x < count; x++)
            {
                reverse.GetNext32BitNumber(3);
                pid = getPIDReverse();
                if (lockInfo[x].nature != 500)
                {
                    genderval = pid & 255;
                    if (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
                        countBackTwo(x);
                }
            }

            forward.Seed = reverse.Seed;
            forward.GetNext32BitNumber();

            //Forwards nature lock check
            for (int x = count2; x >= 0; x--)
            {
                forward.GetNext32BitNumber(3);
                pid = getPIDForward();
                if (lockInfo[x].nature != 500)
                {
                    genderval = pid & 255;
                    if (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
                        countForwardTwo(x);
                }
            }

            return pidOriginal == pid;
        }

        public bool method1SecondShadowShinySkip(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(1);

            uint pidOriginal, psv, psvtemp;
            bool shinyFlag = true;

            //Check how many advances from shiny skip and build initial pid for first nl
            pidOriginal = getPIDReverse();
            psv = ((pidOriginal & 0xFFFF) ^ (pidOriginal >> 16)) >> 3;
            while (shinyFlag)
            {
                pidOriginal = getPIDReverse();
                psvtemp = ((pidOriginal & 0xFFFF) ^ (pidOriginal >> 16)) >> 3;
                if (psvtemp != psv)
                    shinyFlag = false;
                else
                    psv = psvtemp;
            }

            reverse.GetNext32BitNumber(10);
            pidOriginal = getPIDReverse();

            //Backwards nature lock check
            genderval = pidOriginal & 255;
            if (genderval < lockInfo[0].genderLower || genderval > lockInfo[0].genderUpper || pid % 25 != lockInfo[0].nature)
                return false;

            for (int x = 1; x < count; x++)
            {
                reverse.GetNext32BitNumber(3);
                pid = getPIDReverse();
                if (lockInfo[x].nature != 500)
                {
                    genderval = pid & 255;
                    if (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
                        countBackTwo(x);
                }
            }

            forward.Seed = reverse.Seed;
            forward.GetNext32BitNumber();

            //Forwards nature lock check
            for (int x = count2; x >= 0; x--)
            {
                forward.GetNext32BitNumber(3);
                pid = getPIDForward();
                if (lockInfo[x].nature != 500)
                {
                    genderval = pid & 255;
                    if (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
                        countForwardTwo(x);
                }
            }

            return pidOriginal == pid;
        }

        public void method2FirstShadow(bool sister, out uint seed, out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;

            for (int x = count2; x >= 0; x++)
            {
                forwardCounter += 5;
                pid = getPIDForward2(sister);

                if (lockInfo[x].nature != 500)
                {
                    genderval = pid & 255;
                    if (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
                        countForwardTwo2(sister, x);
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

            for (int x = count2; x >= 0; x++)
            {
                forwardCounter += 5;
                pid = getPIDForward2(sister);

                if (lockInfo[x].nature != 500)
                {
                    genderval = pid & 255;
                    if (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
                        countForwardTwo2(sister, x);
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

            for (int x = count2; x >= 0; x++)
            {
                forwardCounter += 5;
                pid = getPIDForward2(sister);

                if (lockInfo[x].nature != 500)
                {
                    genderval = pid & 255;
                    if (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
                        countForwardTwo2(sister, x);
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

            for (int x = count2; x >= 0; x++)
            {
                forwardCounter += 5;
                pid = getPIDForward2(sister);

                if (lockInfo[x].nature != 500)
                {
                    genderval = pid & 255;
                    if (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
                        countForwardTwo2(sister, x);
                }
            }

            forwardCounter += 7;
            pid = getPIDForward2(sister);
            bool shiny = true;
            uint psv, psvtemp;
            psv = ((pid & 0xFFFF) ^ (pid >> 16)) >> 3;
            while (shiny)
            {
                forwardCounter += 2;
                pid = getPIDForward2(sister);
                psvtemp = ((pid & 0xFFFF) ^ (pid >> 16)) >> 3;
                if (psvtemp != psv)
                    shiny = false;
                else
                    psv = psvtemp;
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

            for (int x = count2; x >= 0; x++)
            {
                forwardCounter += 5;
                pid = getPIDShadow();

                if (lockInfo[x].nature != 500)
                {
                    genderval = pid & 255;
                    if (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
                        countForwardTwoShadow(x);
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

            for (int x = count2; x >= 0; x++)
            {
                forwardCounter += 5;
                pid = getPIDShadow();

                if (lockInfo[x].nature != 500)
                {
                    genderval = pid & 255;
                    if (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
                        countForwardTwoShadow(x);
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

            for (int x = count2; x >= 0; x++)
            {
                forwardCounter += 5;
                pid = getPIDShadow();

                if (lockInfo[x].nature != 500)
                {
                    genderval = pid & 255;
                    if (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
                        countForwardTwoShadow(x);
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

            for (int x = count2; x >= 0; x++)
            {
                forwardCounter += 5;
                pid = getPIDShadow();

                if (lockInfo[x].nature != 500)
                {
                    genderval = pid & 255;
                    if (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
                        countForwardTwoShadow(x);
                }
            }

            forwardCounter += 7;
            pid = getPIDShadow();
            bool shiny = true;
            uint psv, psvtemp;
            psv = ((pid & 0xFFFF) ^ (pid >> 16)) >> 3;
            while (shiny)
            {
                forwardCounter += 2;
                pid = getPIDShadow();
                psvtemp = ((pid & 0xFFFF) ^ (pid >> 16)) >> 3;
                if (psvtemp != psv)
                    shiny = false;
                else
                    psv = psvtemp;
            }

            forwardCounter += 7;
            pid = getPIDShadow();
            iv1 = rand[forwardCounter - 4] >> 16;
            iv2 = rand[forwardCounter - 3] >> 16;
        }

        public uint getType(int index)
        {
            switch (index)
            {
                case 0:
                    return 6; //Altaria
                case 1:
                    return 1; //Arbok
                case 2:
                    return 0; //Articuno 
                case 3:
                    return 0; //Baltoy 3
                case 4:
                    return 0; //Baltoy 1
                case 5:
                    return 1; //Baltoy 2
                case 6:
                    return 6; //Banette
                case 7:
                    return 0; //Beedrill
                case 8:
                    return 6; //Butterfree
                case 9:
                    return 0; //Carvanha
                case 10:
                    return 6; //Chansey
                case 11:
                    return 1; //Delcatty
                case 12:
                    return 2; //Dodrio
                case 13:
                    return 1; //Dragonite
                case 14:
                    return 1; //Dugtrio
                case 15:
                    return 1; //Duskull
                case 16:
                    return 1; //Electabuzz
                case 17:
                    return 0; //Exeggutor
                case 18:
                    return 1; //Farfetch'd  
                case 19:
                    return 1; //Golduck
                case 20:
                    return 1; //Grimer
                case 21:
                    return 6; //Growlithe
                case 22:
                    return 1; //Gulpin 3
                case 23:
                    return 1; //Gulpin 1
                case 24:
                    return 1; //Gulpin 2
                case 25:
                    return 1; //Hitmonchan
                case 26:
                    return 1; //Hitmonlee
                case 27:
                    return 0; //Houndour 3
                case 28:
                    return 0; //Houndour 1
                case 29:
                    return 0; //To do houndour 2
                case 30:
                    return 6; //Hypno
                case 31:
                    return 1; //Kangaskhan
                case 32:
                    return 6; //Lapras
                case 33:
                    return 2; //Ledyba
                case 34:
                    return 1; //Lickitung
                case 35:
                    return 0; //Lugia
                case 36:
                    return 1; //Lunatone
                case 37:
                    return 6; //Marcargo
                case 38:
                    return 1; //Magmar 
                case 39:
                    return 1; //Magneton
                case 40:
                    return 1; //Makuhita
                case 41:
                    return 1; //Makuhita Colo
                case 42:
                    return 2; //Manectric
                case 43:
                    return 0; //Mareep 3
                case 44:
                    return 1; //Mareep 1
                case 45:
                    return 1; //Mareep 2
                case 46:
                    return 1; //Marowak
                case 47:
                    return 1; //Mawile
                case 48:
                    return 1; //Meowth
                case 49:
                    return 0; //Moltres
                case 50:
                    return 6; //Mr. Mime
                case 51:
                    return 1; //Natu
                case 52:
                    return 1; //Nosepass
                case 53:
                    return 1; //Numel
                case 54:
                    return 1; //Paras
                case 55:
                    return 1; //Pidgeotto
                case 56:
                    return 2; //Pineco
                case 57:
                    return 6; //Pinsir
                case 58:
                    return 1; //Poliwrath
                case 59:
                    return 2; //Poochyena
                case 60:
                    return 1; //Primeape
                case 61:
                    return 1; //Ralts
                case 62:
                    return 1; //Rapidash
                case 63:
                    return 1; //Raticate
                case 64:
                    return 0; //Rhydon
                case 65:
                    return 1; //Roselia
                case 66:
                    return 6; //Sableye
                case 67:
                    return 3; //Salamence
                case 68:
                    return 1; //Scyther
                case 69:
                    return 0; //To do seedot 3
                case 70:
                    return 1; //Seedot 1
                case 71:
                    return 1; //Seedot 2
                case 72:
                    return 1; //Seel
                case 73:
                    return 0; //Shellder
                case 74:
                    return 1; //Shroomish
                case 75:
                    return 6; //Snorlax
                case 76:
                    return 2; //Snorunt
                case 77:
                    return 1; //Solrock
                case 78:
                    return 1; //Spearow
                case 79:
                    return 1; //Spheal 3
                case 80:
                    return 1; //Spheal 1
                case 81:
                    return 1; //Spheal 2
                case 82:
                    return 1; //Spinarak
                case 83:
                    return 1; //Starmie
                case 84:
                    return 0; //Swellow
                case 85:
                    return 1; //Swinub
                case 86:
                    return 1; //Tangela
                case 87:
                    return 0; //Tauros
                case 88:
                    return 0; //Teddiursa
                case 89:
                    return 0; //Togepi
                case 90:
                    return 0; //Venomoth
                case 91:
                    return 0; //Voltorb
                case 92:
                    return 1; //Vulpix
                case 93:
                    return 6; //Weepinbell
                case 94:
                    return 0; //Zangoose
                default:
                    return 0; //Zapdos 
            }
        }

        private uint getPIDReverse()
        {
            return reverse.GetNext16BitNumber() | (reverse.GetNext32BitNumber() & 0xFFFF0000);
        }

        private uint getPIDForward()
        {
            return (forward.GetNext32BitNumber() & 0xFFFF0000) | forward.GetNext16BitNumber();
        }

        private uint getPIDShadow()
        {
            return (rand[forwardCounter - 1] & 0xFFFF0000) | (rand[forwardCounter] >> 16);
        }

        private uint getPIDForward2(bool sister)
        {
            return sister ? ((rand[forwardCounter - 1] & 0xFFFF0000) | (rand[forwardCounter] >> 16)) ^ 0x80008000 : (rand[forwardCounter - 1] & 0xFFFF0000) | (rand[forwardCounter] >> 16);
        }

        private void countBackTwo(int x)
        {
            pid = getPIDReverse();
            genderval = pid & 255;
            while (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
            {
                pid = getPIDReverse();
                genderval = pid & 255;
            }
        }

        private void countForwardTwo(int x)
        {
            pid = getPIDForward();
            genderval = pid & 255;
            while (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
            {
                pid = getPIDForward();
                genderval = pid & 255;
            }
        }

        private void countForwardTwo2(bool sister, int x)
        {
            forwardCounter += 2;
            pid = getPIDForward2(sister);
            genderval = pid & 255;
            while (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
            {
                forwardCounter += 2;
                pid = getPIDForward2(sister);
                genderval = pid & 255;
            }
        }

        private void countForwardTwoShadow(int x)
        {
            forwardCounter += 2;
            pid = getPIDShadow();
            genderval = pid & 255;
            while (genderval < lockInfo[x].genderLower || genderval > lockInfo[x].genderUpper || pid % 25 != lockInfo[x].nature)
            {
                forwardCounter += 2;
                pid = getPIDShadow();
                genderval = pid & 255;
            }
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