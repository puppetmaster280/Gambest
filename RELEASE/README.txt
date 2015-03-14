GAMBITS V4 (release 4.1)

Chapter 1: --Introduction--
Chapter 2: --xml Heirachy--
Chapter 3: --Trigger List--
Chapter 4: --Reaction List--



CHAPTER 1: --Introduction--
Gambits is a powerful bot tool for final fantasy 11.

However, to utilize it, you will need to edit the main.xml
file, and learn how to manipulate it.

First and foremost, gambits runs an AI system that parses
an xml (see main.xml for an example to work from) and performs
actions based on the data within that xml file.

Gambits can run multiple accounts off of one xml, as seen in the
examples, these are bound by <player> tags. Inside of a player tags
lies the brunt of the AI. These are labelled as Gambits, and
represent a priority list of triggers bounded to reactions. These
Program continuously scans the list of gambits from top to bottom,
and whenever it finds a gambit which has all of its trigger logic
up, it will perform the attached reaction, then return to the top

There are two halves to each gambit, a list of triggers (as low as 1,
and as many as you want) and an attached reaction. The triggers are
passed through a logic gate, and if the end product outputs TRUE, the
program performs the attached reaction, then returns back to the top.

This is the basis of Gambits v4, please read all of the info below to
fully utilize the program, it is very finicky and will crash if you
format your xml incorrectly.

Chapter 2: --xml Heirachy--
The heirachy of the xml is divided up as follows:

ROOT
--SETTINGS
  --GLOBAL DELAY
  --PARTY MEMBER COUNT

--PLAYER
  --GAMBIT
    --TRIGGERS
	  --TRIGGER
	--REACTION

=ROOT=
This encapsulates the whole xml file, for parsing by the program.

=SETTINGS=
This holds global settings

=PARTY MEMBER COUNT=
How many total party members there are (including you), this is
important as FFACE detects HP of party members who have left,
IE you may have 6 members, then one leaves. FFACE will still think
they are there under some situations. This stops that issue.

=PLAYER=
Args: Name(string), Leader(0/1)
This holds the AI for a player by name. This is part of gambits
multibox function. You can put as many players in one xml as you want.
Name: Name of the player
Leader: Whether this is a leader player or not. Leader players will
get feedback from gambits in their chat log, non-leaders wont.

=GAMBIT=
This holds one 'gambit', which is a list of triggers and reactions.

=TRIGGERS=
arg: gate (AND/OR {NOT}) (NOT AND, AND NOT, AND, NOT OR, OR NOT, OR are all valid)
This holds the list of triggers. These will be passed through an above logic gate.

AND: All values must be true
OR: At least one value must be true
NOT: Takes the value and 'flips' it. True becomes False, False becomes True.

=TRIGGER=
Comes in three varieties, <SELF.../>, <PARTY.../>, and <TARGET.../>
Use the above depending on your chosen trigger, see the trigger list in 
chapter 3 for correct binding.

args:type(see trigger list), gate(less, greater, equals), arg(see trigger list)

=REACTION=
args: type(see reaction list), arg(see reaction list)

The most important thing to remember, is your reaction, if it has a target, 
will use the last target found from the triggers. See example.xml.

Example 1: Last trigger is a SELF trigger, reaction will use on self.

Example 2: Last trigger is a SELF trigger, will attempt to use box step on
self (wont work)

Example 3: Last trigger is a TARGET trigger, will use box step on target (works)

Example 4: This gambit will heal party members with hp <50%, very powerful utility!

Chapter 3: --Trigger List--

=HP=
Targets: <SELF> <PARTY>
Gets raw HP of target (NOT a percent)
Argument: integer value

=HPP=
Targets: <SELF> <PARTY> <TARGET>
Gets raw HP% of target (IS a percent)
Argument: integer value 0-100

=MP=
Targets: <SELF> <PARTY>
Gets raw MP of target (NOT a percent)
Argument: integer value

=MPP=
Targets: <SELF> <PARTY> <TARGET>
Gets raw MP% of target (IS a percent)
Argument: integer value 0-100

=TP=
Targets: <SELF> <PARTY>
Gets raw TP of target (is NOT a percent)
Argument: integer value 0-1000

=NAME=
Targets: <TARGET>
Returns true if target's name CONTAINS argument.
Argument: String. Not case sensitive.

=STATUS=
Targets: <SELF>
Returns true if your status is Argument
Argument: String, list of statuses can be found in state.txt

=DISTANCE=
Targets: <TARGET>
Gets distance between you and target.
Argument: Integer (or float I guess, whatever)

=EFFECT=
Targets: <SELF>
Returns true if you have argument's stat effect on you
Argument: See Effects.txt for list

=ASSIST=
Targets: <TARGET>
Performs a /asssist on 'argument', if your target CHANGES, it returns true
Argument: string, player name, case sensitive.

Chapter 4: --Reaction List--
=ATTACK=
Performs /attack on target

=SPELL=
Casts a spell of name "argument", write it as you would in game
Example: <REACTION type="SPELL" arg="Cure II" />

=ABILITY=
Uses an ability of name "argument", write it as you would in game
Example: <REACTION type="ABILITY" arg="Provoke" />

=WEAPONSKILL=
Performs a weaponskill of name "argument" if you have >1000 tp, but
if you have <1000 tp, it skips and continues. Write as in game.
Example: <REACTION type="WEAPONSKILL" arg="Rudra's Storm" />

=INPUT=
Inputs the string "argument" into chat log. Powerful utility.
Example: <REACTION type="INPUT" arg="/heal" />

=TRACK=
Locks on to target, then follows them until closer than distance of Argument
example: <REACTION type="TRACK" arg="5" /> (will engage up to 5' of monster)

=FIND=
Tabs around until it finds an NPC with name CONTAINING 'argument'
(not case sensitive)
example: <REACTION type="FIND" arg="Crab" />





