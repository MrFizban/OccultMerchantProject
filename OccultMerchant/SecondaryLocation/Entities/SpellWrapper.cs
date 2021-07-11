using System;
using System.ComponentModel.DataAnnotations;

namespace SecondaryLocation.Entities
{

        public class SpellWrappper
        {
            [Key]
            public Guid id { get; set; }
            public int range { get; set; }
            public string target { get; set; }
            public string duration { get; set; }
            public string savingThrow { get; set; }
            public bool spellResistence { get; set; }
            public string casting { get; set; }
            public string component { get; set; }
            public string school { get; set; }
            public string level { get; set; }


            public SpellWrappper()
            {
            }

            public SpellWrappper(Guid id)
            {
                this.id = id;
            }
            public SpellWrappper(Spell spell)
            {
                this.id = spell.id;
                this.range = spell.range;
                this.target = spell.target;
                this.duration = spell.duration;
                this.duration = spell.duration;
                this.savingThrow = spell.savingThrow;
                this.casting = spell.casting;
                this.component = spell.component;
                this.school = spell.school;
                this.level = spell.level;
            }

        }
    }
