require 'q_1_6'

describe "#strip_whitespace" do
  it "removes all whitespace" do
    str = "   three ninjas attack!  "
    expected = "threeninjasattack!"
    expect(str.strip_whitespace).to eq expected
  end
end
