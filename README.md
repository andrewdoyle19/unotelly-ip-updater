unotelly-ip-updater
===================

This is a console application, which can be installed as a Windows Service which updates your dynamic IP address with your Unotelly account (http://www.unotelly.com) when it changes

How To Install
===================

Firstly you will need to obtain your unotelly user hash and update the app.config file.

Visit https://quickstart3.unotelly.com/networks and you will see IP Update link which looks something like http://www.unotelly.com/unodns/auto_auth/hash_update/updateip.php?user_hash=lfgdflgkndgdfggfdg4c26asd9

Copy the query string value (everything after =) and replace the default XXXX  in the app settings of the App.config file

You can also configure your logging directory here in the nlog settings.

