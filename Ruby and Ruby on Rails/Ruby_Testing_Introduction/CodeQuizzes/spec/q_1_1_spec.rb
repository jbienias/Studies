require "q_1_1"

describe "#word_count" do
  it "returns number of words in a string" do
    string = "laughter, it's free"
    expect(word_count(string)).to eq 3
  end

  it "returns 0 for empty string" do
    expect(word_count("")).to eq 0
  end
end
