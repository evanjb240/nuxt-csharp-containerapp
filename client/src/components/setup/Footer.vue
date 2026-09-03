<template>
    <footer class="site-footer">

        <section class="footer-cta">
            <div class="footer-cta-content">
                <p class="eyebrow">LET'S BUILD SOMETHING</p>

                <h2>Ready to Start Your Project?</h2>

                <p>
                    From the first conversation to the final walkthrough,
                    we're here to make your project a success.
                </p>

                <button to="/contactus" class="footer-cta-button" @click="requestEstimate = !requestEstimate">
                    Request a Free Estimate
                </button>
                <ContactUsForm alignment="align-center flex-column"  v-show="requestEstimate"/>
            </div>
        </section>

        <section class="footer-main">

            <div class="footer-brand">
                <BusinessLogo color="black" :width="50" />

                <p>
                    Some tagline here
                </p>

                <a href="tel:+14145551234">
                    (414) 555-1234
                </a>
            </div>

            <div class="footer-column">
                <h3>Services</h3>

                <NuxtLink to="/about">
                    Planning
                </NuxtLink>

                <NuxtLink to="/Profile">
                    Modeling
                </NuxtLink>
            </div>

            <div class="footer-column">
                <h3>Company</h3>

                <NuxtLink to="/About">
                    About Us
                </NuxtLink>

                <NuxtLink to="/ContactUs">
                    Contact Us
                </NuxtLink>
            </div>

            <div class="footer-column">
                <h3>Service Area</h3>

                <span>Milwaukee, WI</span>
            </div>

        </section>

        <section class="footer-bottom">
            <span>
                © {{ new Date().getFullYear() }}  LLC.
                All rights reserved.
            </span>

        </section>

    </footer>
</template>
<script setup lang="ts">
import BusinessLogo from '~/components/business/BusinessLogo.vue';
import ContactUsForm from '~/components/form/ContactUsForm.vue';
const requestEstimate = ref(false);
</script>

<style scoped>
.site-footer {
    width: 100%;
    margin: 0;
    padding: 0;

    background: #111;
    color: white;
}

.footer-cta {
    width: 100%;
    box-sizing: border-box;

    padding: 70px 20px;
    text-align: center;

    background: #1c1c1c;
}

.footer-cta-content {
    width: 100%;
    max-width: 750px;
    margin: 0 auto;
}

.eyebrow {
    margin: 0 0 15px;
    font-size: 13px;
    font-weight: 700;
    letter-spacing: 3px;
    opacity: 0.6;
}

.footer-cta h2 {
    margin: 0;
    font-size: clamp(36px, 5vw, 64px);
    line-height: 1.05;
    font-family: 'Baliva';
}

.footer-cta-content > p:not(.eyebrow) {
    max-width: 600px;
    margin: 25px auto 35px;

    font-size: 18px;
    line-height: 1.6;
    color: #ccc;
}

.footer-cta-button {
    display: inline-block;
    padding: 15px 30px;
    background: #1c1c1c;
    font-size: 16px;

    border: 2px solid white;

    color: white;
    text-decoration: none;

    font-weight: 700;
    text-transform: uppercase;
    letter-spacing: 1px;
}


/* MAIN */

.footer-main {
    width: 100%;
    max-width: 1400px;

    margin: 0 auto;
    padding: 60px 30px;

    box-sizing: border-box;

    display: grid;
    grid-template-columns:
        minmax(0, 2fr)
        minmax(0, 1fr)
        minmax(0, 1fr)
        minmax(0, 1fr);

    gap: 50px;
}

.footer-brand,
.footer-column {
    min-width: 0;
}

.footer-brand {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
}

.footer-brand p {
    max-width: 320px;
    margin: 20px 0;

    color: #aaa;
    line-height: 1.6;
}

.footer-brand a {
    color: white;
    text-decoration: none;
}

.footer-brand a + a {
    margin-top: 8px;
}

.footer-column {
    display: flex;
    flex-direction: column;
    gap: 10px;
}

.footer-column h3 {
    margin: 0 0 10px;

    font-size: 14px;
    text-transform: uppercase;
    letter-spacing: 2px;
}

.footer-column a,
.footer-column span {
    color: #999;
    text-decoration: none;
    font-size: 15px;
}


/* BOTTOM */

.footer-bottom {
    width: 100%;
    max-width: 1400px;

    margin: 0 auto;
    padding: 20px 30px;

    box-sizing: border-box;

    border-top: 1px solid #292929;

    display: flex;
    justify-content: space-between;
    gap: 20px;

    color: #666;
    font-size: 13px;
}

.footer-bottom div {
    display: flex;
    gap: 25px;
}

.footer-bottom a {
    color: #666;
    text-decoration: none;
}


/* TABLET */

@media screen and (max-width: 800px) {
    .footer-main {
        grid-template-columns: 1fr 1fr;
    }

    .footer-brand {
        grid-column: 1 / -1;
    }
}


/* MOBILE */

@media screen and (max-width: 480px) {
    .footer-cta {
        padding: 55px 20px;
    }

    .footer-main {
        grid-template-columns: 1fr;
        padding: 45px 25px;
    }

    .footer-brand {
        grid-column: auto;
    }

    .footer-bottom {
        flex-direction: column;
        padding: 20px 25px;
    }
}
</style>