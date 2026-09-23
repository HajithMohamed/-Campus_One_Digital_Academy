from pathlib import Path
from PIL import Image, ImageOps, ImageDraw

folder = Path(__file__).resolve().parent / "RenderedReport"
files = sorted(folder.glob("page-*.png"))
for start in range(0, len(files), 4):
    sheet = Image.new("RGB", (900, 1200), "#777777")
    draw = ImageDraw.Draw(sheet)
    for index, path in enumerate(files[start:start + 4]):
        page = ImageOps.contain(Image.open(path).convert("RGB"), (420, 545))
        x = 20 + (index % 2) * 450
        y = 35 + (index // 2) * 580
        sheet.paste(page, (x, y))
        draw.text((x, 12 + (index // 2) * 580), path.stem, fill="white")
    sheet.save(folder / f"contact-{start // 4 + 1}.png")
