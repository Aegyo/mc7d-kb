using System;
using System.Collections.Generic;
using System.IO;
using static _3dedit.Keybindings;


namespace _3dedit
{

    public class Keybindings
    {

        public Dictionary<string, KeybindSet> keybinds = new Dictionary<string, KeybindSet>();
        public KeybindSet activeKeybinds;
        public string activeKeybindsName;

        public static Dictionary<string, KeybindSet> defaultBinds = new Dictionary<string, KeybindSet> {
            { "5D_2key",  new KeybindSet(new Dictionary<string, IAction> {
                { "D", new Grip(Axis.W, 1) },
                { "V", new Grip(Axis.W, -1) },
                { "S", new Grip(Axis.X, 1) },
                { "F", new Grip(Axis.X, -1) },
                { "E", new Grip(Axis.Y, 1) },
                { "C", new Grip(Axis.Y, -1) },
                { "W", new Grip(Axis.Z, -1) },
                { "R", new Grip(Axis.Z, 1) },
                { "A", new Grip(Axis.V, 1) },
                { "G", new Grip(Axis.V, -1) },
                { "X", new Grip(Axis.W, 0b11111) },

                { "J", new Twist(Axis.X, Axis.Z) },
                { "L", new Twist(Axis.Z, Axis.X) },

                { "K", new Twist(Axis.Z, Axis.Y) },
                { "I", new Twist(Axis.Y, Axis.Z) },

                { "U", new Twist(Axis.Y, Axis.X) },
                { "O", new Twist(Axis.X, Axis.Y) },

                { "Y", new Twist(Axis.V, Axis.Y) },
                { "H", new Twist(Axis.Y, Axis.V) },

                { "M", new Twist(Axis.V, Axis.X) },
                { "Oemcomma", new Twist(Axis.X, Axis.V) },

                { "N", new Twist(Axis.Z, Axis.V) },
                { "OemPeriod", new Twist(Axis.V, Axis.Z) },

                { "Space", new Recenter() },

                { "D1", new Layer(1) },
                { "D2", new Layer(2) },
                { "D3", new Layer(4) },
                { "ShiftKey", new Layer(1|2) },
            })},
            { "Default_3key", new KeybindSet(new Dictionary<string, IAction>{
                { "D", new Grip(Axis.W, 1) },
                { "V", new Grip(Axis.W, -1) },
                { "S", new Grip(Axis.X, 1) },
                { "F", new Grip(Axis.X, -1) },
                { "E", new Grip(Axis.Y, 1) },
                { "C", new Grip(Axis.Y, -1) },
                { "W", new Grip(Axis.Z, -1) },
                { "R", new Grip(Axis.Z, 1) },
                { "A", new Grip(Axis.V, 1) },
                { "G", new Grip(Axis.V, -1) },
                { "Q", new Grip(Axis.U, 1) },
                { "T", new Grip(Axis.U, -1) },
                { "Z", new Grip(Axis.T, 1) },
                { "B", new Grip(Axis.T, -1) },
                { "X", new Grip(Axis.W, 0b11111) },

                { "L", new Twist2c(Axis.X, true) },
                { "K", new Twist2c(Axis.Y, false) },
                { "J", new Twist2c(Axis.Z, true) },
                { "O", new Twist2c(Axis.V, true) },
                { "I", new Twist2c(Axis.U, false) },
                { "U", new Twist2c(Axis.T, false) },

                { "Space", new Recenter() },

                { "D1", new Layer(1) },
                { "D2", new Layer(2) },
                { "D3", new Layer(4) },
            }) }
        };

        public Keybindings()
        {
            foreach (var item in defaultBinds)
            {
                keybinds.Add(item.Key, item.Value);
            }
            switchKeybindSet("5D_2key");
        }

        public IAction GetAction(string key)
        {
            bool res = activeKeybinds.binds.TryGetValue(key, out IAction action);
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
        public bool switchKeybindSet(string name)
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
                    string k = item.Key;
                    res.Add($"{k},{item.Value.Serialize()}");
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
                            action = new Grip();
                            break;
                        case "Twist":
                            action = new Twist();
                            break;
                        case "Recenter":
                            action = new Recenter();
                            break;
                        case "GripTwist":
                            action = new GripTwist();
                            break;
                        case "Twist2c":
                            action = new Twist2c();
                            break;
                        case "Layer":
                            action = new Layer();
                            break;
                    }

                    if (action != null)
                    {
                        string k = p2[0];
                        action.Deserialize(item);
                        binds.Add(k, action);
                    }
                }
            }
        }

        public interface IAction {
            void OnKeyDown(ref Cube7D Cube, ref bool redraw, ref bool didTwist);
            void OnKeyUp(ref Cube7D Cube, ref bool redraw, ref bool didTwist);

            string Serialize();
            void Deserialize(string s);
        }


        public class Twist : IAction
        {
            public Axis fromAxis;
            public Axis toAxis;

            public Twist()
            {
                this.fromAxis = Axis.X;
                this.toAxis = Axis.Y;
            }
            public Twist(Axis fromAxis, Axis toAxis) {
                this.fromAxis = fromAxis;
                this.toAxis = toAxis;
            }

            public void OnKeyDown(ref Cube7D Cube, ref bool redraw, ref bool didTwist)
            {
                if (Cube.Gripped[0] == -1)
                {
                    return;
                }

                if (Cube.D < fromAxis.idx || Cube.D < toAxis.idx)
                {
                    return;
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

                redraw = true;
                didTwist = true;
            }

            public void OnKeyUp(ref Cube7D Cube, ref bool redraw, ref bool didTwist) { }

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

            public Grip()
            {
                this.axis = Axis.X;
                this.layerMask = 1;
            }
            public Grip(Axis axis, int layerMask){
                this.axis = axis;
                this.layerMask = layerMask;
            }


            public void OnKeyDown(ref Cube7D Cube, ref bool redraw, ref bool didTwist)
            {
                Cube.Grip(axis.idx, layerMask);
                redraw = true;
            }
            public void OnKeyUp(ref Cube7D Cube, ref bool redraw, ref bool didTwist)
            {
                int m0 = Cube.NormGripMask(layerMask);
                if (Cube.Gripped[0] == axis.idx && Cube.Gripped[1] == m0)
                {
                    Cube.Grip(-1, 1);
                    redraw = true;
                }
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

        public class Recenter : IAction
        {
            public void OnKeyDown(ref Cube7D Cube, ref bool redraw, ref bool didTwist)
            {
                if (Cube.Gripped[0] != -1)
                {
                    Cube.RotateCubeByGrip();
                    redraw = true;
                }
            }
            public void OnKeyUp(ref Cube7D Cube, ref bool redraw, ref bool didTwist) { }

            public string Serialize() { return "Recenter"; }
            public void Deserialize(string s) { }
        }

        public class GripTwist : IAction
        {
            private Grip grip;
            private Twist twist;

            public GripTwist()
            {
                this.grip = new Grip();
                this.twist = new Twist();
            }
            public GripTwist(Axis gripAxis, int layerMask, Axis fromAxis, Axis toAxis)
            {
                this.grip = new Grip(gripAxis, layerMask);
                this.twist = new Twist(fromAxis, toAxis);
            }

            public void OnKeyDown(ref Cube7D Cube, ref bool redraw, ref bool didTwist) { }
            public void OnKeyUp(ref Cube7D Cube, ref bool redraw, ref bool didTwist)
            {
                this.grip.OnKeyDown(ref Cube, ref redraw, ref didTwist);
                this.twist.OnKeyDown(ref Cube, ref redraw, ref didTwist);
                this.twist.OnKeyUp(ref Cube, ref redraw, ref didTwist);
                this.grip.OnKeyUp(ref Cube, ref redraw, ref didTwist);
            }

            public string Serialize()
            {
                return $"GripTwist,{this.grip.axis.name},{this.grip.layerMask},{this.twist.fromAxis.name},{this.twist.toAxis.name}";
            }
            public void Deserialize(string s)
            {
                string[] p = s.Split(',');
                if (p[1] != "GripTwist" || !Axis.fromString.ContainsKey(p[2]) || !Axis.fromString.ContainsKey(p[4]) || !Axis.fromString.ContainsKey(p[5]))
                {
                    throw new Exception($"Invalid GripTwist: {s}");
                }

                this.twist.fromAxis = Axis.fromString[p[4]];
                this.twist.toAxis = Axis.fromString[p[5]];

                Int32.TryParse(p[3], out int mask);
                this.grip.layerMask = mask;
                this.grip.axis = Axis.fromString[p[2]];
            }
        }

        public class Twist2c : IAction
        {
            public Axis axis;
            public bool negative;


            public Twist2c()
            {
                this.axis = Axis.X;
                this.negative = false;
            }
            public Twist2c(Axis axis, bool negative)
            {
                this.axis = axis;
                this.negative = negative;
            }

            public void OnKeyDown(ref Cube7D Cube, ref bool redraw, ref bool didTwist)
            {
                Twist t = Cube.partialTwist;

                if (t.fromAxis == null)
                {
                    t.fromAxis = this.axis;
                }
                else
                {
                    t.toAxis = this.axis;
                }

                if (this.negative)
                {
                    Axis tmp = t.toAxis;
                    t.toAxis = t.fromAxis;
                    t.fromAxis = tmp;
                }

                if (t.toAxis != null && t.fromAxis != null)
                {
                    t.OnKeyDown(ref Cube, ref redraw, ref didTwist);
                    t.OnKeyUp(ref Cube, ref redraw, ref didTwist);
                    t.toAxis = null;
                    t.fromAxis = null;

                    redraw = true;
                    didTwist = true;
                }
            }
           public void OnKeyUp(ref Cube7D Cube, ref bool redraw, ref bool didTwist) { }

            public string Serialize()
            {
                return $"Twist2c,{(this.negative ? "-" : "+")},{axis.name}";
            }
            public void Deserialize(string s)
            {
                string[] p = s.Split(',');
                if (p[1] != "Twist2c" || !Axis.fromString.ContainsKey(p[3]))
                {
                    throw new Exception($"Invalid Twist2c: {s}");
                }

                this.axis = Axis.fromString[p[3]];
                this.negative = p[2] == "-";
            }
        }

        public class Layer : IAction
        {
            public int layerMask;

            public Layer()
            {
                this.layerMask = 1;
            }
            public Layer(int layerMask)
            {
                this.layerMask = layerMask;
            }

            public void OnKeyDown(ref Cube7D Cube, ref bool redraw, ref bool didTwist)
            {
                Cube.LayerOverrides.Add(this);
                redraw = true;
            }
            public void OnKeyUp(ref Cube7D Cube, ref bool redraw, ref bool didTwist)
            {
                Cube.LayerOverrides.Remove(this);
                redraw = true;
            }

            public string Serialize()
            {
                return $"Layer,{layerMask}";
            }
            public void Deserialize(string s)
            {
                string[] p = s.Split(',');
                if (p[1] != "Layer")
                {
                    throw new Exception($"Invalid Layer: {s}");
                }
                
                Int32.TryParse(p[2], out int mask);
                this.layerMask = mask;
            }
        }
    }
}
