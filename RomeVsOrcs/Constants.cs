using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using SharpDX.DirectWrite;
using SharpDX.MediaFoundation;
using SharpDX.XAudio2;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System;

namespace RomeVsOrcs;
public static class Constants
{
    private static int numberOfKills = 0;

    public static int NumberOfKills
    {
        get => numberOfKills;
        set
        {
            numberOfKills = value;
            Rank = GetRank();
        }
    }

    public static string Rank { get; private set; } = GetRank();

    private static string GetRank()
    {
        return numberOfKills switch
        {
            >= 192 => "Legatus",
            >= 128 => "Tribunus",
            >= 96 => "Centurio",
            >= 64 => "Optio",
            >= 32 => "Aquilifer",
            >= 16 => "Signifer",
            >= 8 => "Praetorian Guard",
            >= 4 => "Equites",
            >= 1 => "Miles",
            0 => "Velites",
            _ => "Velites",
        };
    }
}

//Legatus

//Senior officer, often in command of a legion or a large military unit.
//Appointed by the Roman Senate or the Emperor, typically from the senatorial class.
//Responsible for strategic decisions, military operations, and diplomatic relations.
//Acts as a liaison between the military and political authorities.

//Tribunus

//A junior officer rank, often serving as a staff officer or in command of a cohort.
//Typically comprised of six tribunes per legion, often from the equestrian class.
//Responsible for training soldiers, maintaining discipline, and leading troops in battle.
//Often used as a stepping stone for future political careers.

//Centurio

//A centurion commands a century, which is about 80-100 soldiers.
//Key figure in maintaining discipline and training within the ranks.
//Promoted based on merit and experience, often from the ranks of the miles.
//Plays a crucial role in battlefield tactics and troop movements.

//Optio

//Second-in-command to a centurion, assisting in leadership and training.
//Often responsible for administrative tasks and maintaining order within the century.
//Selected for their experience and leadership potential, often promoted from the ranks.
//Acts as a key communicator between the centurion and the soldiers.

//Aquilifer

//Standard bearer of the legion, responsible for carrying the eagle standard (aquila).
//Symbolizes the honor and spirit of the legion, crucial for morale.
//Holds a prestigious position, often selected for bravery and loyalty.
//Plays a vital role in maintaining formation and rallying troops during battle.

//Signifer

//Standard bearer for a century, carrying the unit's standard (signum).
//Responsible for communication and signaling during battles.
//Often serves as a paymaster, managing the distribution of wages to soldiers.
//Holds a respected position within the ranks, often chosen for reliability.

//Miles

//The basic soldier in the Roman army, typically a heavy infantryman.
//Fought in formations, primarily the manipular system, and later the cohort system.
//Required to be a Roman citizen, often equipped with armor and weapons.
//Played a crucial role in the expansion and defense of the Roman Empire.

//Velites

//Light infantry skirmishers, often used for harassment and reconnaissance.
//Typically younger soldiers, not yet fully equipped as heavy infantry.
//Armed with javelins and lighter armor, providing flexibility on the battlefield.
//Played a key role in the early phases of battle, disrupting enemy formations.

//Equites

//Cavalry units, often composed of wealthier citizens who could afford horses.
//Served as scouts, flanking forces, and in pursuit of fleeing enemies.
//Played a significant role in battles, providing mobility and speed.
//Often held a social status above infantry soldiers, with many becoming officers.

//Praetorian Guard

//Elite unit tasked with protecting the Emperor and high-ranking officials.
//Comprised of highly trained soldiers, often selected for their loyalty and skill.
//Played a significant role in political power dynamics, sometimes influencing succession.
//Served as both a military and police force within Rome, maintaining order.