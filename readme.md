# PasswordChangeAssistant
[![Version](https://img.shields.io/github/release/rookiestyle/passwordchangeassistant)](https://github.com/rookiestyle/passwordchangeassistant/releases/latest)
[![Releasedate](https://img.shields.io/github/release-date/rookiestyle/passwordchangeassistant)](https://github.com/rookiestyle/passwordchangeassistant/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/rookiestyle/passwordchangeassistant/total?color=%2300cc00)](https://github.com/rookiestyle/passwordchangeassistant/releases/latest/download/PasswordChangeAssistant.plgx)\
[![License: GPL v3](https://img.shields.io/github/license/rookiestyle/passwordchangeassistant)](https://www.gnu.org/licenses/gpl-3.0)

PasswordChangeAssistant is a KeePass plugin that offers multiple assistance functions all related to changing passwords:
- Simplify changing passwords 
- Highlight the most recently used password profile per entry
- Synchronize password profiles as part your database

# Table of Contents
- [Configuration](#configuration)
- [Usage](#usage)
- [Translations](#translations)
- [Download and Requirements](#download-and-requirements)

# Configuration
PasswordChangeAssistant is designed to *simply work* and does not require any kind of configuration for changing passwords.
  
Only for synchronizing password profiles, PasswordChangeAssistant integrates into KeePass' options form.  

# Usage
A more detailed description of the different functions can be found in the [Wiki](https://github.com/rookiestyle/passwordchangeassistant/wiki)
## Changing passwords
PasswordChangeAssistant integrates into the entry form and offers a standalone form in addition.

Within the enry form, PasswordChangeAssistant displays a different icon in front of the most recently used password profile.  
![Last used profile](images/PasswordChangeAssistant%20-%20last%20profile.png)

In addition, a new button to auto-type/copy both old and new passwords is displayed
![Entry form integration](images/PasswordChangeAssistant%20-%20entry%20form.png)

The stand alone form can be accessed using the context menu und supports [PEDCalc](https://github.com/rookiestyle/pedcalc) as well.
It offers additional functionality like 
- 2nd URL fields that can hold the direct link to the *Change password* site of the selected entry
- Select - and maintain - Auto-type sequences for changing the password
![Standalone form](images/PasswordChangeAssistant%20-%20standalone%20form.png)


## Synchronizing password profiles
Here you can move existing profiles in various directions
- Between two databases
- From an open database to the global KeePass configuration
- From the global KeePass configuration to an open database

![Profile sync](images/PasswordChangeAssistant%20-%20Options.png)

# Translations
PasswordChangeAssistant is provided with english language built-in and allow usage of translation files.
These translation files need to be placed in a folder called *Translations* inside in your plugin folder.
If a text is missing in the translation file, it is backfilled with the english text.
You're welcome to add additional translation files by creating a pull request.

Naming convention for translation files: `<plugin name>.<language identifier>.language.xml`\
Example: `PasswordChangeAssistant.de.language.xml`
  
The language identifier in the filename must match the language identifier inside the KeePass language that you can select using *View -> Change language...*\
This identifier is shown there as well, if you have [EarlyUpdateCheck](https://github.com/rookiestyle/earlyupdatecheck) installed.

# Download and Requirements
## Download
Please follow these links to download the plugin file itself.
- [Download newest release](https://github.com/rookiestyle/passwordchangeassistant/releases/latest/download/PasswordChangeAssistant.plgx)
- [Download history](https://github.com/rookiestyle/passwordchangeassistant/releases)

If you're interested in any of the available translations in addition, please download them from the [Translations](Translations) folder.
## Requirements
* KeePass: 2.40
