using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Divine;
using Divine.Menu;
using Divine.Menu.Items;
using System.Threading.Tasks;
using Divine.SDK.Helpers;
using Divine.SDK.Extensions;
using SharpDX;
using Divine.Menu.EventArgs;
using System.Windows.Input;
using System.Windows.Media;


namespace KingMorphling
{

    class MainMenu 
    {

        public Abuse abuse;
        public MenuSwitcher menuSwitcher;
        private Combo combo;
        private Shift shift;
        private Hero LocalHero;
        private Save save;
        private AutoShift autoShift;
        public static MenuSwitcher shiftSwitcher;
        public MenuSwitcher saveSwitcher;
        public MenuSwitcher autoshiftSwitcher;
        public static MenuSlider shiftSlider2; 
        public static MenuHoldKey HpToggleIntervalhd;
        public MenuSlider Size;
        public MenuSlider PositionX;
        public MenuSlider PositionY;
        public static MenuSlider Perc;
        private MenuHoldKey comboHold;
        private MenuSpellToggler menuItems;
        private MenuSpellToggler menuSpell;

        public bool IsMove;
        public static bool saveOn;
        public static bool menuSwitcherOn;
        public static int perkValue;
        public static int perkValue2;


        public MainMenu()
        {
            
            //Morph on
            menuSwitcherOn = false;
            var rootMenu = MenuManager.CreateRootMenu("Morphling");
            menuSwitcher = rootMenu.CreateSwitcher("On/Off");
            menuSwitcher.ValueChanged += MenuSwitcher_ValueChanged;
            saveOn = false;
            var menuSelector2 = rootMenu.CreateMenu("UltSave");
            saveSwitcher = menuSelector2.CreateSwitcher("Autosave");
            saveSwitcher.ValueChanged += MenuSwitcher_ValueChanged3;
            shiftSlider2 = menuSelector2.CreateSlider("minHptoSave", 40, 5, 50);
            var menuSelector3 = rootMenu.CreateMenu("Autoshift stun");
            autoshiftSwitcher = menuSelector3.CreateSwitcher("On/Off");
            autoshiftSwitcher.ValueChanged += AutoshiftSwitcher_ValueChanged;
            //Balance
            var menuSelector = rootMenu.CreateMenu("Auto balance");
            shiftSwitcher = menuSelector.CreateSwitcher("Autoshift");
            shiftSwitcher.ValueChanged += MenuSwitcher_ValueChanged2;
            //panel
            var panel = menuSelector.CreateMenu("Panel settings");
            PositionX = panel.CreateSlider("PositionX", 500, 0, 10000);
            PositionY = panel.CreateSlider("PositionY", 500, 0, 10000);
            Size = panel.CreateSlider("Size", 50, 0, 100);
            Perc = panel.CreateSlider("Perc", 500, 0, 6000);
            Perc.ValueChanged += Perc_ValueChanged;
            Perc.ValueChanged += Perc_ValueChanged1;
            //Combo
            var menuSelector4 = rootMenu.CreateMenu("Combo");
            comboHold = menuSelector4.CreateHoldKey("ComboHold", Key.None);
            comboHold.ValueChanged += ComboHold_ValueChanged;
            menuSpell = menuSelector4.CreateSpellToggler("Spells", ListSpellsToggler, false);
            menuItems = menuSelector4.CreateSpellToggler("Item",ListItemsToggler,false);
            
            //Morph let me die
            HpToggleIntervalhd = rootMenu.CreateHoldKey("Abuse(BETA)",Key.None);
            HpToggleIntervalhd.ValueChanged += HpToggleIntervalhd_ValueChanged;
           
        }
        public static Dictionary<AbilityId, bool> ListSpellsToggler = new Dictionary<AbilityId, bool>
        {
                { AbilityId.morphling_adaptive_strike_agi,true },
                { AbilityId.morphling_waveform,true }
        };

        public static Dictionary<AbilityId, bool> ListItemsToggler = new Dictionary<AbilityId, bool>
            {
                { AbilityId.item_black_king_bar,true},
                { AbilityId.item_bloodthorn,true},
                { AbilityId.item_mjollnir,true},
                { AbilityId.item_nullifier,true},
                { AbilityId.item_ethereal_blade,true},
                { AbilityId.item_manta,true},
                { AbilityId.item_diffusal_blade,true},
                { AbilityId.item_essence_ring,true},
                { AbilityId.item_minotaur_horn ,true}
            };
        private void ComboHold_ValueChanged(MenuHoldKey holdKey, HoldKeyEventArgs e)
        {
            if (combo == null)
            {
                combo = new Combo();
            }

            if (e.Value)
            {
                combo.Enable();
            }
            else
            {
                combo.Disable();
            }
        }

        private void Perc_ValueChanged1(MenuSlider slider, SliderEventArgs e)
        {
           if(e.NewValue != e.OldValue)
            {
                perkValue2 = Perc.Value;
            }
            
        }

        private void HpToggleIntervalhd_ValueChanged(MenuHoldKey menuToggleKey, HoldKeyEventArgs e)
        {
            if (abuse == null)
            {
                abuse = new Abuse();
            }

            if(e.Value)
            {
                abuse.Enable();
            }
            else
            {
                abuse.Disable();
            }
        }

       
        private void Perc_ValueChanged(MenuSlider slider, SliderEventArgs e)
        {
            perkValue = Perc.Value;
            
        }

       

        private void AutoshiftSwitcher_ValueChanged(MenuSwitcher switcher, SwitcherEventArgs e)
        {
            if(autoShift == null)
            {
                autoShift = new AutoShift();
            }
            
            if(e.Value)
            {

                autoShift.Enable();

            }
            else
            {

                autoShift.Disable();
            }
        }

      

        private void MenuSwitcher_ValueChanged(MenuSwitcher switcher, SwitcherEventArgs e)
        {
            if (e.Value)
            {

                menuSwitcherOn = true;
                
            }
            else
            {
                menuSwitcherOn = false;
                Deactiv();
            }
        }
        private void MenuSwitcher_ValueChanged2(MenuSwitcher switcher, SwitcherEventArgs e)
        {
            if(shift == null)
            {
                shift = new Shift();
            }
            if (e.Value)
            {

                shift.Enable();
                OnActivate();
                
            }
            else
            {
                shift.Disable();
                Deactiv();
            }
        }
      
        private void MenuSwitcher_ValueChanged3(MenuSwitcher switcher, Divine.Menu.EventArgs.SwitcherEventArgs e)
        {
            if(save == null)
            {
                save = new Save();
            }
            if (e.Value)
            {

                save.Enable();

            }
            else
            {
                save.Disable();
            }
        }
      

        private void OnActivate()
        {


            InputManager.MouseKeyDown += InputManager_MouseKeyDown;
            InputManager.MouseKeyUp += InputManager_MouseKeyUp;
            InputManager.MouseMove += InputManager_MouseMove;

            RendererManager.Draw += OnRendererManagerDraw;
        }



        private void InputManager_MouseKeyDown(MouseEventArgs e)
        {
            if (e.MouseKey != MouseKey.Left)
            {
                return;
            }

            var scaling = RendererManager.Scaling + ((Size - 20) * 0.02f);
            var rect = new RectangleF(PositionX, PositionY, 500 * scaling, 150 * scaling);
            var firstRect = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height * 0.39f);
            var secondRect = new RectangleF(rect.X, rect.Y + firstRect.Height, rect.Width, rect.Height - firstRect.Height);

            if (e.Position.IsUnderRectangle(secondRect))
            {
                IsMove = true;
                //Perc.Value = (int)((e.Position.X - secondRect.X) / secondRect.Width * (Perc.MaxValue - Perc.MinValue) + Perc.MinValue);
            }
            //else if ( e.Position.IsUnderRectangle(new RectangleF(rect.X -5, rect.Y - 5 , rect.Width, rect.Height - firstRect.Height)))
            //{
            //    IsMove = false;
                
            //}
            

        }

        private void InputManager_MouseKeyUp(MouseEventArgs e)
        {
            if (e.MouseKey != MouseKey.Left)
            {
                return;
            }

            IsMove = false;
        }

        private void InputManager_MouseMove(MouseMoveEventArgs e)
        {
            if (!IsMove)
            {
                return;
            }

            var scaling = RendererManager.Scaling + ((Size - 20) * 0.02f);
            var rect = new RectangleF(PositionX, PositionY, 500 * scaling, 150 * scaling);
            var firstRect = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height * 0.39f);
            var secondRect = new RectangleF(rect.X, rect.Y + firstRect.Height, rect.Width, rect.Height - firstRect.Height);

            Perc.Value = (int)((e.Position.X - secondRect.X) / secondRect.Width * (Perc.MaxValue - Perc.MinValue) + Perc.MinValue);
        }

        public void OnRendererManagerDraw()
        {
            LocalHero = EntityManager.LocalHero;
            var maxMorphAGI = Math.Floor(LocalHero.Agility);
            var maxMorphSTR = Math.Floor(LocalHero.Strength);
            var curentMaxHp = LocalHero.MaximumHealth;
            var minHP = curentMaxHp - maxMorphSTR * 19;
            var maxHP = curentMaxHp + maxMorphAGI * 19;
            
            
            var scaling = RendererManager.Scaling + ((Size - 20) * 0.02f);
            
            var rect = new RectangleF(PositionX , PositionY, 500 * scaling, 150 * scaling);
            var lineWidth = 5 * scaling;
            var lineColor = Color.DarkSlateBlue;
            var backgroundColor = Color.Black;
            var fontFamilyName = "Tahoma";
            var percText = Perc.Value.ToString();
            var minHPText = minHP.ToString();
            var maxHPText = maxHP.ToString();
           
            RendererManager.DrawFilledRectangle(rect, lineColor, backgroundColor , lineWidth);
            var firstRect = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height * 0.39f);
            RendererManager.DrawLine(new Vector2(firstRect.X, firstRect.Y + firstRect.Height), new Vector2(firstRect.X + firstRect.Width, firstRect.Y + firstRect.Height), lineColor, lineWidth);
            RendererManager.DrawText(percText, firstRect, Color.Green, fontFamilyName, FontFlags.Center | FontFlags.VerticalCenter, 36 * scaling);

            var secondRect = new RectangleF(rect.X, rect.Y + firstRect.Height, rect.Width, rect.Height - firstRect.Height);
            //RendererManager.DrawText(minHPText, secondRect, Color.Red, fontFamilyName, FontFlags.VerticalCenter, 40 * scaling);
            RendererManager.LoadTextureFromAssembly("KingMorphling.Rectangle.png", "KingMorphling.Resources.Rectangle.png");
            RendererManager.DrawTexture("KingMorphling.Rectangle.png", secondRect);
            var maxHPTextSize = RendererManager.MeasureText(maxHPText, fontFamilyName, 40 * scaling);
            //RendererManager.DrawText(
            //    maxHPText,
            //    new RectangleF(secondRect.X + secondRect.Width - maxHPTextSize.X, secondRect.Y, secondRect.Width, secondRect.Height),
            //    Color.Red,
            //    fontFamilyName,
            //    FontFlags.VerticalCenter,
            //    36 * scaling);

            var perc = (float)Perc;
            var progresLine = secondRect.X + (perc / 6000f * secondRect.Width);

            if (IsMove)
            {
                RendererManager.DrawLine(new Vector2(progresLine, secondRect.Y), new Vector2(progresLine, secondRect.Y + secondRect.Height), Color.Aqua, lineWidth * 2);
            }

            RendererManager.DrawLine(new Vector2(progresLine, secondRect.Y), new Vector2(progresLine, secondRect.Y + secondRect.Height), Color.Black, lineWidth);
        }
        public void Deactiv()
        {
            InputManager.MouseKeyDown -= InputManager_MouseKeyDown;
            InputManager.MouseKeyUp -= InputManager_MouseKeyUp;
            InputManager.MouseMove -= InputManager_MouseMove;

            RendererManager.Draw -= OnRendererManagerDraw;
        }
    }
}

