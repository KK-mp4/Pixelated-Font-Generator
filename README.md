# Pixeleted Font Generator (work in progress)
## Generating automatically full Unicode pixeleted font

Recently I made a full font family called [Pixie](https://kkmp4.gumroad.com/l/pixie), its main idea was to be the smallest readable font and although I think I succseeded at that task, idea never left that that font does not include full Unicode table, plus where are my guarantees that those pixelated shapes are truly the best way to represent corresponding glyph?

So from drawing I turned to programming, first thing I did is generating an image with all possible pixel combinatioins for 3 fonts
<br/>**3x4:**<br/>
![192x256_RGB](https://user-images.githubusercontent.com/103208695/180891512-6eeb7427-1f92-4e12-bdc1-c36cbc77ff9e.png)
<br/>**3x5:**<br/>
![546x910_RGB](https://user-images.githubusercontent.com/103208695/180891515-7de5dd00-f126-4f0c-97f5-c6889c987208.png)
<br/>**4x4:**<br/>
![1024x1024_RGB](https://user-images.githubusercontent.com/103208695/180891516-9ca562dc-7b11-450a-861d-fed48542a3f7.png)

Although this is fun it wasn't really useful, so next thing i did is finding closest match for an imput image, that way I can go though full unicode and find closest corresponding pixeleted match, let's look at example 3x4 shape:<br/>
![image](https://user-images.githubusercontent.com/103208695/180893246-ac66458d-74f4-47e9-ac0a-a7d98a3726a7.png)

Then I generated an upscaled version using [signed distance fields](https://en.wikipedia.org/wiki/Signed_distance_function):<br/>
![image](https://user-images.githubusercontent.com/103208695/180893170-7c8fca5b-c6f6-4fe8-ae69-334a29018a0d.png)

And now I can binaraze this image to get the upscaled shape<br/>
![image](https://user-images.githubusercontent.com/103208695/180893201-37c35669-d95c-43b7-9632-4e2ccb90f720.png)

Using this algorithm I can calculate difference between two images and find closest match, for example here is imput letter M:<br/>
![image](https://user-images.githubusercontent.com/103208695/180892474-647bc2fd-d04d-40cb-aff9-7e4fc42b1a72.png)

And here is closest match it found:<br/>
![image](https://user-images.githubusercontent.com/103208695/180892572-22925071-f7b7-4df5-8174-61c2df544df8.png)

And upscaled version:<br/>
![image](https://user-images.githubusercontent.com/103208695/180892553-8df0b9b9-535f-474a-b6e9-c3c4ff1fffc5.png)

This is still work in progress but its definitely a fun project to work on
