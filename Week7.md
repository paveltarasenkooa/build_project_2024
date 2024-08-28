# Working on Design. Polishing user experience.

## Let's add some colors! 

Main file that contains style for our application is located in "wwwroot" -> "css" folder: site.css.

Lets add some fance background to our appllication using gradient: 

Add next lines to ste.css file "body" section: 

```css
  body {
      margin-bottom: 60px;
      background: #6a11cb;
      /* NEW LINES ADDED BELOW*/
      background: linear-gradient(to top, rgba(106, 17, 203, 1), rgba(37, 117, 252, 1));
      color: white !important;
  }
```

To pick your own color you can use [colorpicker](https://rgbacolorpicker.com/);

Let's add some styles to our header menu: 

add style for <nav> element:

```css
    nav {
        background: linear-gradient(to top, rgba(37, 117, 252, 1), rgba(106, 17, 203, 1));
        color: white !important;
    }

```


