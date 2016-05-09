<!DOCTYPE html>
<html>

<head>
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>Sound Account</title>
  <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
  <link rel="stylesheet" type="text/css" href="css/main/main.css">
  <link rel="stylesheet" type="text/css" href="css/main/footer.css">
  <link rel="stylesheet" type="text/css" href="css/forms/forms.css">
  <link rel="stylesheet" type="text/css" href="css/main/fixedmenu.css">
</head>

<body>
  <div id="header" class="scrolled" class="container-fluid">
    <img id="logo" class="logo" src="images/logo.png" />
    <ul id="nav">
      <li><a href="index.html">INFO</a></li>
      <li><a href="/css/">PROGRAM</a></li>
      <li><a href="/js/">TICKETS</a></li>
      <li><a href="/jquery/">CAMPING</a></li>
    </ul>
  </div>
  <div id="title" class=" container-fluid">
    <p id="title-text">
      SOUND ACCOUNT
    </p>
  </div>
  <div id="main" class="container-fluid">

    <div id="buttons-form-container" class="acc-button col-xs-10 col-md-4 col-xs-offset-1 col-md-offset-4">
      <div class="row">
        <div class="acc-button col-md-4 col-xs-4 col-md-offset-1 col-xs-offset-1">
          <a href="#">
            <p id="sign-in-link" class="acc-button-text">SIGN IN</p>
          </a>
        </div>
        <div class="acc-button col-md-4 col-xs-4 col-md-offset-2 col-xs-offset-2">
          <a href="#">
            <p id="register-link" class="acc-button-text">REGISTER</p>
          </a>
        </div>
      </div>
      <!--Here are loaded the forms  -->
      <div id="form-container">

      </div>
    </div>
  </div>

  </div>
  <div id="footer" class="container-fluid">
    <img id="footerlogo" src="images/LOGOFOOT.png" />
    <ul id="footernav">
      <li><a href="#">NEWS & SOCIAL</a></li>
      <li><a href="#">CONTACT US</a></li>
      <li><a href="#">TERMS AND PRIVACY</a></li>
    </ul>
    <div id="social">
      <img id="footerfacebook" src="images/facebook.png" />
      <img id="footertwitter" src="images/twitter.png" />
      <img id="footergplus" src="images/gplus.png" />
    </div>
    <p id="copyright">COPYRIGHT:2016</p>
  </div>

  <script src="https://code.jquery.com/jquery-2.2.2.min.js"></script>
  <script src="js/form-loader.js"></script>
  <script src="js/auth/ajaxLogin.js"></script>
  <script src="js/auth/ajaxRegister.js"></script>
  </script>
</body>

</html>
