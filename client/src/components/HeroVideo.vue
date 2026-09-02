
<template>
  <section class="hero">
    <!-- Background video -->
    <video
      class="hero-video"
      autoplay
      muted
      loop
      playsinline
      preload="auto"
    >
      <source
        src="https://test-videos.co.uk/vids/bigbuckbunny/mp4/h264/720/Big_Buck_Bunny_720_10s_1MB.mp4"
        type="video/mp4"
      />
    </video>

    <!-- Darkens the video so text is readable -->
    <div class="hero-overlay"></div>

    <!-- Hero content -->
    <div class="hero-content">
      <h1>Whittern Construction</h1>

      <p>
        Proudly serving the Aleutian Islands with 30+ years of local construction experience.
      </p>

      <div class="hero-buttons">
        <NuxtLink to="/contactus" class="primary">
          Request a Quote
        </NuxtLink>

        <NuxtLink to="/ourprocess" class="secondary">
          Learn More
        </NuxtLink>
      </div>
    </div>
  </section>
</template>

<style scoped>
.hero {
  position: relative;
  width: 100%;
  height: 600px;
  overflow: hidden;

  display: flex;
  align-items: center;
  justify-content: center;
}

.hero-video {
  position: absolute;
  inset: 0;

  width: 100%;
  height: 100%;

  object-fit: cover;
  object-position: center;

  z-index: 0;
}

.hero-overlay {
  position: absolute;
  inset: 0;

  background: rgba(0, 0, 0, 0.45);

  z-index: 1;
}

.hero-content {
  position: relative;
  z-index: 2;

  width: 100%;
  max-width: 900px;

  padding: 2rem;

  text-align: center;
  color: white;
}

.hero-content h1 {
  margin: 0 0 1rem;

  font-size: clamp(2.5rem, 6vw, 5rem);
  font-weight: 700;
  line-height: 1.05;
}

.hero-content p {
  margin: 0 auto 2rem;

  max-width: 650px;

  font-size: clamp(1rem, 2vw, 1.35rem);
  line-height: 1.6;
}

.hero-buttons {
  display: flex;
  justify-content: center;
  gap: 1rem;
}

.hero-buttons a {
  padding: 0.9rem 1.75rem;

  border-radius: 0.5rem;
  text-decoration: none;
  font-size: 1rem;
  font-weight: 600;

  cursor: pointer;
}

.primary {
  border: none;
  background: white;
  color: black;
}

.secondary {
  border: 1px solid rgba(255, 255, 255, 0.7);
  background: transparent;
  color: white;
}

@media (max-width: 600px) {
  .hero {
    height: 500px;
  }

  .hero-buttons {
    flex-direction: column;
    align-items: center;
  }

  .hero-buttons button {
    width: 200px;
  }
}
</style>