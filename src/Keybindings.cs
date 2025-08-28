using System;
using System.Collections.Generic;
using System.IO;


namespace _3dedit
{

    public class Keybindings
    {

        public Dictionary<string, KeybindSet> keybinds = new Dictionary<string, KeybindSet>();
        public KeybindSet activeKeybinds;
        public string activeKeybindsName;

        public static KeybindSet defaultBinds = new KeybindSet(new Dictionary<string, IAction>
        {
            { "D", new Grip(Axis.W, 1) },
            { "V", new Grip(Axis.W, 4) },
            { "S", new Grip(Axis.X, 1) },
            { "F", new Grip(Axis.X, 4) },
            { "E", new Grip(Axis.Y, 1) },
            { "C", new Grip(Axis.Y, 4) },
            { "W", new Grip(Axis.Z, 4) },
            { "R", new Grip(Axis.Z, 1) },
            { "A", new Grip(Axis.V, 1) },
            { "G", new Grip(Axis.V, 4) },

            { "J", new Twist(Axis.X, Axis.Z) },
            { "L", new Twist(Axis.Z, Axis.X) },

            { "K", new Twist(Axis.Z, Axis.Y) },
            { "I", new Twist(Axis.Y, Axis.Z) },

            { "U", new Twist(Axis.Y, Axis.X) },
            { "O", new Twist(Axis.X, Axis.Y) },

            { "Y", new Twist(Axis.W, Axis.Y) },
            { "H", new Twist(Axis.Y, Axis.W) },

            { "M", new Twist(Axis.W, Axis.X) },
            { ",", new Twist(Axis.X, Axis.W) },

            { "N", new Twist(Axis.Z, Axis.W) },
            { ".", new Twist(Axis.W, Axis.Z) },
        });

        public Keybindings()
        {
            keybinds.Add("__Default", defaultBinds);
            switchKeybindSet("__Default");
        }

        public IAction GetAction(string key)
        {
            IAction action = null;
            bool res = activeKeybinds.binds.TryGetValue(key, out action);
            return action;
        }

        public string Serialize()
        {
            List<string> res = new List<string>();

            foreach (var item in keybinds)
            {
                res.Add($"{item.Key} : {item.Value.Serialize()}");
            }

            return string.Join("\r\n", res.ToArray());
        }

        public void LoadKeybindSet(string s)
        {
            string[] p = s.Split(new string[] { " : " }, StringSplitOptions.None);
            KeybindSet kbs = new KeybindSet();
            kbs.Deserialize(p[1]);

            if (keybinds.ContainsKey(p[0]))
            {
                keybinds.Remove(p[0]);
            }
            keybinds.Add(p[0], kbs);

            if (keybinds.Count == 1)
            {
                switchKeybindSet(p[0]);
            }
        }
        bool switchKeybindSet(string name)
        {
            if (keybinds.ContainsKey(name))
            {
                activeKeybinds = keybinds[name];
                activeKeybindsName = name;

                return true;
            }
            return false;
        }

        public class Axis
        {
            public static readonly Axis X = new Axis("X", 2), Y = new Axis("Y", 4), Z = new Axis("Z", 3), W = new Axis("W", 1), V = new Axis("V", 5), U = new Axis("U", 6), T = new Axis("T", 7);
            public static readonly Dictionary<string, Axis> fromString = new Dictionary<string, Axis>()
            {
                { "X", X },
                { "Y", Y },
                { "Z", Z },
                { "W", W },
                { "V", V },
                { "U", U },
                { "T", T }
            };

            public string name;
            public int idx;

            public Axis(string name, int idx)
            {
                this.name = name;
                this.idx = idx;
            }
        }

        public class KeybindSet
        {
            public Dictionary<string, IAction> binds = new Dictionary<string, IAction>();

            public KeybindSet() { }
            public KeybindSet(Dictionary<string, IAction> binds) {
                this.binds = binds;
            }

            public string Serialize()
            {
                List<string> res = new List<string>();

                foreach (var item in binds)
                {
                    res.Add($"{item.Key},{item.Value.Serialize()}");
                }

                return String.Join(" ", res.ToArray());
            }

            public void Deserialize(string data)
            {
                string[] p = data.Split(' ');

                foreach (var item in p)
                {
                    string[] p2 = item.Split(',');
                    if (item.Length < 2) continue;

                    IAction action = null;

                    switch (p2[1])
                    {
                        case "Grip":
                            action = new Grip(Axis.X, 1);
                            break;
                        case "Twist":
                            action = new Twist(Axis.X, Axis.X);
                            break;
                    }

                    if (action != null)
                    {
                        action.Deserialize(item);
                        binds.Add(p2[0], action);
                    }
                }
            }
        }

        public interface IAction {
            bool OnKeyPress(ref Cube7D Cube);
            bool OnKeyDown(ref Cube7D Cube);
            bool OnKeyUp(ref Cube7D Cube);

            string Serialize();
            void Deserialize(string s);
        }


        public class Twist : IAction
        {
            public Axis fromAxis;
            public Axis toAxis;

            public Twist(Axis fromAxis, Axis toAxis) {
                this.fromAxis = fromAxis;
                this.toAxis = toAxis;
            }

            public bool OnKeyPress(ref Cube7D Cube)
            {
                if (Cube.Gripped[0] == -1)
                {
                    return false;
                }

                int from = fromAxis.idx;
                int to = toAxis.idx;
                bool flip = false;

                if (Cube.Gripped[0] == from)
                {
                    from = Cube.Gripped[0] == Axis.W.idx ? Axis.V.idx : Axis.W.idx;
                    flip = true;
                }
                if (Cube.Gripped[0] == to)
                {
                    to = Cube.Gripped[0] == Axis.W.idx ? Axis.V.idx : Axis.W.idx;
                    flip = true;
                }
                if (flip && Cube.Gripped[1] == 1)
                {
                    int tmp = from;
                    from = to;
                    to = tmp;
                }

                Cube.TwistGrip(from, to);

                return true;
            }

            public bool OnKeyDown(ref Cube7D Cube) { return false; }
            public bool OnKeyUp(ref Cube7D Cube) { return false;  }

            public string Serialize()
            {
                return $"Twist,{fromAxis.name},{toAxis.name}";
            }

            public void Deserialize(string s)
            {
                string[] p = s.Split(',');
                if (p[1] != "Twist" || !Axis.fromString.ContainsKey(p[2]) || !Axis.fromString.ContainsKey(p[3]))
                {
                    throw new Exception($"Invalid Twist: {s}");
                }

                fromAxis = Axis.fromString[p[2]];
                toAxis = Axis.fromString[p[3]];
            }
        }

        public class Grip : IAction
        {
            public Axis axis;
            public int layerMask;

            public Grip(Axis axis, int layerMask){
                this.axis = axis;
                this.layerMask = layerMask;
            }


            public bool OnKeyPress(ref Cube7D Cube) { return false; }
            public bool OnKeyDown(ref Cube7D Cube)
            {
                Cube.Grip(axis.idx, layerMask);
                return true;
            }
            public bool OnKeyUp(ref Cube7D Cube)
            {
                if (Cube.Gripped[0] == axis.idx && Cube.Gripped[1] == layerMask)
                {
                    Cube.Grip(-1, 1);
                    return true;
                }
                return false;
            }

            public string Serialize()
            {
                return $"Grip,{axis.name},{layerMask}";
            }

            public void Deserialize(string s)
            {

                string[] p = s.Split(',');
                if (p[1] != "Grip" || !Axis.fromString.ContainsKey(p[2]))
                {
                    throw new Exception($"Invalid Grip: {s}");
                }

                int mask;
                Int32.TryParse(p[3], out mask);
                layerMask = mask;

                axis = Axis.fromString[p[2]];
            }
        }

    }
}
